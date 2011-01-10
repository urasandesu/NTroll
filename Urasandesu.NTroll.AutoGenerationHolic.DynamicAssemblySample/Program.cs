using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Reflection;
using System.IO;
using System.Reflection.Emit;

namespace Urasandesu.NTroll.AutoGenerationHolic.DynamicAssemblySample
{
    class Program
    {
        static int Main(string[] args)
        {
            // dynamic assembly 版 Equals メソッド、GetHashCode メソッドを自動生成処理。
            // 指定されたアセンブリを読み取り、EqualityTargetAttribute カスタム属性を持つ型を列挙。
            // その型が持つプロパティを読み取り、EqualityTargetAttribute カスタム属性を持つプロパティに絞込み。
            // 上記のプロパティを使って、Equals メソッド、GetHashCode メソッドを自動生成する。
            // 加えて、中身を再現することはできないので、ルールをあらかじめ決めておく必要がある。
            // ここでは、以下のルールを設けた。
            // - 参照するアセンブリは固定。
            // - プロパティと対応するフィールドが存在する。フィールド名はプロパティ名の頭文字を小文字化したもの。
            // - フィールドのアクセス修飾子は Private。
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Urasandesu.NTroll.AutoGenerationHolic.DynamicAssemblySample.exe <TargetAssemblyPath> <OutputAssemblyPath>");
                return -1;
            }

            var targetAssemblyPath = args[0];
            var outputAssemblyPath = args[1];
            var outputAssemblyFileName = Path.GetFileName(outputAssemblyPath);
            var outputAssemblyFileNameWithoutExtension = Path.GetFileNameWithoutExtension(outputAssemblyPath);

            var assemblyName = new AssemblyName(outputAssemblyFileNameWithoutExtension);
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            var module = assembly.DefineDynamicModule(outputAssemblyFileNameWithoutExtension, outputAssemblyFileName);
            foreach (var t in Assembly.LoadFrom(targetAssemblyPath).GetTypesDefined<EqualityTargetAttribute>())
            {
                var type = BuildType(module, t);
                var properties = BuildProperties(type, t);
                BuildMethodEquals(type, t, properties);
                BuildMethodGetHashCode(type, t, properties);
                type.CreateType();
            }

            assembly.Save(outputAssemblyFileName);
            var lastWriteTimeOfFile = File.GetLastWriteTime(outputAssemblyFileName);
            var lastWriteTimeOfPath = File.GetLastWriteTime(outputAssemblyPath);
            if (lastWriteTimeOfPath < lastWriteTimeOfFile)
            {
                File.Copy(outputAssemblyFileName, outputAssemblyPath, true);
            }

            return 0;
        }

        static TypeBuilder BuildType(ModuleBuilder module, Type t)
        {
            var type = module.DefineType(t.FullName, t.Attributes, t.BaseType, t.GetInterfaces());
            return type;
        }

        static PropertyBuilder[] BuildProperties(TypeBuilder type, Type t)
        {
            var properties = new List<PropertyBuilder>();

            foreach (var p in t.GetProperties())
            {
                var field = type.DefineField(p.Name.ToCamel(), p.PropertyType, FieldAttributes.Private);
                var property = type.DefineProperty(
                    p.Name, p.Attributes, p.PropertyType, p.GetIndexParameters().Select(_ => _.ParameterType).ToArray());
                property.SetSetMethod(BuildPropertySetter(type, field, p));
                property.SetGetMethod(BuildPropertyGetter(type, field, p));
                if (p.IsDefined(typeof(EqualityTargetAttribute), false))
                {
                    properties.Add(property);
                }
            }

            return properties.ToArray();
        }

        static MethodBuilder BuildPropertySetter(TypeBuilder type, FieldBuilder field, PropertyInfo p)
        {
            var sm = p.GetSetMethod();
            var method = type.DefineMethod(sm.Name, sm.Attributes);
            method.SetReturnType(typeof(void));
            method.SetParameters(sm.GetParameters().Select(_ => _.ParameterType).ToArray());
            var value = method.DefineParameter(1, ParameterAttributes.None, "value");
            var gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        static MethodBuilder BuildPropertyGetter(TypeBuilder type, FieldBuilder field, PropertyInfo p)
        {
            var gm = p.GetGetMethod();
            var method = type.DefineMethod(gm.Name, gm.Attributes);
            method.SetReturnType(gm.ReturnType);
            var gen = method.GetILGenerator();
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        static void BuildMethodEquals(TypeBuilder type, Type t, PropertyBuilder[] properties)
        {
            if (properties.Length == 0) return;

            var methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
            var method = type.DefineMethod("Equals", methodAttributes);
            method.SetReturnType(typeof(Boolean));
            method.SetParameters(typeof(Object));
            var obj = method.DefineParameter(1, ParameterAttributes.None, "obj");
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
            if (properties.Length == 1)
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
        }

        static void BuildMethodGetHashCode(TypeBuilder type, Type t, PropertyBuilder[] properties)
        {
            if (properties.Length == 0) return;

            var methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
            var method = type.DefineMethod("GetHashCode", methodAttributes);
            method.SetReturnType(typeof(Int32));
            var gen = method.GetILGenerator();
            var getHashCode =
                typeof(Object).GetMethod(
                    "GetHashCode",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                    null,
                    Type.EmptyTypes,
                    null
                );

            if (properties.Length == 1)
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
        }
    }
}
