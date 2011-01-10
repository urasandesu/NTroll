using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;

namespace Urasandesu.NTroll.AutoGenerationHolic.DynamicMethodSample
{
    public class MethodInjector<T> where T : class, IDelegateEqualityComparerable<T>
    {
        DelegateEqualityComparer<T> comparer;
        public DelegateEqualityComparer<T> Comparer 
        {
            get 
            {
                if (comparer == null)
                {
                    var properties = typeof(T).GetPropertiesDefined<EqualityTargetWithComparerAttribute>().ToArray();
                    var equalsProvider = BuildMethodEquals(properties);
                    var getHashCodeProvider = BuildMethodGetHashCode(properties);
                    comparer = new DelegateEqualityComparer<T>(equalsProvider, getHashCodeProvider);
                }
                return comparer; 
            } 
        }

        public T Setup(T o) 
        {
            var c = (IDelegateEqualityComparerable<T>)o;
            c.Comparer = Comparer;
            return o;
        }

        Func<T, object, bool> BuildMethodEquals(PropertyInfo[] properties)
        {
            var method = new DynamicMethod(string.Empty, typeof(bool), new Type[] { typeof(T), typeof(object) });
            var type = typeof(T);
            var gen = method.GetILGenerator();
            var that = gen.DeclareLocal(type);                  // 0
            var areEqual = gen.DeclareLocal(typeof(Boolean));   // 1
            var isThat = gen.DeclareLocal(typeof(Boolean));     // 2
            var labelIsThat = gen.DefineLabel();
            var labelReturn = gen.DefineLabel();
            var labelAreNotEqual = gen.DefineLabel();
            var labelStAreEqual = gen.DefineLabel();

            // var that = default(Customer);
            // if ((that = obj as Customer) != null) return false;
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Isinst, type);
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Stloc_2);
            gen.Emit(OpCodes.Ldloc_2);
            gen.Emit(OpCodes.Brtrue_S, labelIsThat);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Stloc_1);
            gen.Emit(OpCodes.Br_S, labelReturn);
            gen.MarkLabel(labelIsThat);

            // return this.Property1 == that.Property1 &&
            //        this.Property2 == that.Property2 &&
            //        this.Property3 == that.Property3 && ...
            if (properties.Length == 0)
            {
                var baseEquals = type.BaseType.GetMethod("Equals");
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Callvirt, baseEquals);
                gen.MarkLabel(labelReturn);
                gen.Emit(OpCodes.Ret);
            }
            else if (properties.Length == 1)
            {
                var gm = properties[0].GetGetMethod();
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Callvirt, gm);
                if (gm.ReturnType.HasOptimizedOpEquality())
                {
                    gen.Emit(OpCodes.Bne_Un_S, labelAreNotEqual);
                }
                else
                {
                    var opEquality = gm.ReturnType.GetMethodOpEquality();
                    gen.Emit(OpCodes.Call, opEquality);
                    gen.Emit(OpCodes.Brfalse_S, labelAreNotEqual);
                }
                gen.Emit(OpCodes.Ldc_I4_1);
                gen.Emit(OpCodes.Br_S, labelReturn);
                gen.MarkLabel(labelAreNotEqual);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.MarkLabel(labelReturn);
                gen.Emit(OpCodes.Ret);
            }
            else if (2 <= properties.Length)
            {
                var gm = default(MethodInfo);
                var opEquality = default(MethodInfo);
                gm = properties[0].GetGetMethod();
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Callvirt, gm);
                if (!gm.ReturnType.HasOptimizedOpEquality())
                {
                    opEquality = gm.ReturnType.GetMethodOpEquality();
                    gen.Emit(OpCodes.Call, opEquality);
                }

                for (int i = 1; i < properties.Length; i++)
                {
                    if (gm.ReturnType.HasOptimizedOpEquality())
                    {
                        gen.Emit(OpCodes.Bne_Un_S, labelAreNotEqual);
                    }
                    else
                    {
                        gen.Emit(OpCodes.Brfalse_S, labelAreNotEqual);
                    }

                    gm = properties[i].GetGetMethod();
                    opEquality = gm.ReturnType.GetMethodOpEquality();
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Callvirt, gm);
                    if (!gm.ReturnType.HasOptimizedOpEquality())
                    {
                        opEquality = gm.ReturnType.GetMethodOpEquality();
                        gen.Emit(OpCodes.Call, opEquality);
                    }
                }
                gen.Emit(OpCodes.Br_S, labelStAreEqual);
                gen.MarkLabel(labelAreNotEqual);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.MarkLabel(labelStAreEqual);
                gen.Emit(OpCodes.Stloc_1);
                gen.Emit(OpCodes.Br_S, labelReturn);
                gen.MarkLabel(labelReturn);
                gen.Emit(OpCodes.Ldloc_1);
                gen.Emit(OpCodes.Ret);
            }
            return (Func<T, object, bool>)method.CreateDelegate(typeof(Func<T, object, bool>));
        }

        Func<T, int> BuildMethodGetHashCode(PropertyInfo[] properties)
        {
            var method = new DynamicMethod(string.Empty, typeof(int), new Type[] { typeof(T) });
            var gen = method.GetILGenerator();
            var getHashCode =
                typeof(Object).GetMethod(
                    "GetHashCode",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    Type.EmptyTypes,
                    null
                );

            if (properties.Length == 0)
            {
                var baseGetHashCode = typeof(T).BaseType.GetMethod("GetHashCode");
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Callvirt, baseGetHashCode);
                gen.Emit(OpCodes.Ret);
            }
            else if (properties.Length == 1)
            {
                var gm = properties[0].GetGetMethod();
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                if (gm.ReturnType.IsValueType)
                {
                    var local = gen.DeclareLocal(gm.ReturnType);
                    gen.Emit(OpCodes.Stloc, local);
                    gen.Emit(OpCodes.Ldloca_S, local.LocalIndex);
                    gen.Emit(OpCodes.Constrained, gm.ReturnType);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                }
                else
                {
                    var labelNullIfClass = gen.DefineLabel();
                    var labelNotNullIfClass = gen.DefineLabel();
                    gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                    gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                    gen.MarkLabel(labelNullIfClass);
                    gen.Emit(OpCodes.Ldc_I4_0);
                    gen.MarkLabel(labelNotNullIfClass);
                }
                gen.Emit(OpCodes.Ret);
            }
            else if (2 <= properties.Length)
            {
                var gm = default(MethodInfo);
                var labelNullIfClass = default(Label);
                var labelNotNullIfClass = default(Label);

                gm = properties[0].GetGetMethod();
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                if (gm.ReturnType.IsValueType)
                {
                    var local = gen.DeclareLocal(gm.ReturnType);
                    gen.Emit(OpCodes.Stloc, local);
                    gen.Emit(OpCodes.Ldloca_S, local.LocalIndex);
                    gen.Emit(OpCodes.Constrained, gm.ReturnType);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                }
                else
                {
                    labelNullIfClass = gen.DefineLabel();
                    labelNotNullIfClass = gen.DefineLabel();
                    gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                    gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                    gen.MarkLabel(labelNullIfClass);
                    gen.Emit(OpCodes.Ldc_I4_0);
                    gen.MarkLabel(labelNotNullIfClass);
                }

                for (int i = 1; i < properties.Length; i++)
                {
                    gm = properties[i].GetGetMethod();
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    if (gm.ReturnType.IsValueType)
                    {
                        var local = gen.DeclareLocal(gm.ReturnType);
                        gen.Emit(OpCodes.Stloc, local);
                        gen.Emit(OpCodes.Ldloca_S, local.LocalIndex);
                        gen.Emit(OpCodes.Constrained, gm.ReturnType);
                        gen.Emit(OpCodes.Callvirt, getHashCode);
                    }
                    else
                    {
                        labelNullIfClass = gen.DefineLabel();
                        labelNotNullIfClass = gen.DefineLabel();
                        gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                        gen.Emit(OpCodes.Ldarg_0);
                        gen.Emit(OpCodes.Call, gm);
                        gen.Emit(OpCodes.Callvirt, getHashCode);
                        gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                        gen.MarkLabel(labelNullIfClass);
                        gen.Emit(OpCodes.Ldc_I4_0);
                        gen.MarkLabel(labelNotNullIfClass);
                    }
                    gen.Emit(OpCodes.Xor);
                }
                gen.Emit(OpCodes.Ret);
            }
            return (Func<T, int>)method.CreateDelegate(typeof(Func<T, int>));
        }
    }
}
