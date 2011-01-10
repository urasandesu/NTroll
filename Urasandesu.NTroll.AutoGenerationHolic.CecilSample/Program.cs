using System;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Urasandesu.NTroll.AutoGenerationHolic.CecilSample.Mixins.Mono.Cecil;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;

namespace Urasandesu.NTroll.AutoGenerationHolic.CecilSample
{
    class Program
    {
        static int Main(string[] args)
        {
            // cecil 版 Equals メソッド、GetHashCode メソッドを自動生成処理。
            // 指定されたアセンブリを読み取り、EqualityTargetAttribute カスタム属性を持つ型を列挙。
            // その型が持つプロパティを読み取り、EqualityTargetAttribute カスタム属性を持つプロパティに絞込み。
            // 上記のプロパティを使って、Equals メソッド、GetHashCode メソッドを自動生成する。
            // cecil の場合、中身を走査しながらの再構築ができるため、新しく作成するメソッドのみを生成することが可能。
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Urasandesu.NTroll.AutoGenerationHolic.CecilSample.exe <TargetAssemblyPath> <OutputAssemblyPath>");
                return -1;
            }

            var targetAssemblyPath = args[0];
            var targetAssemblyFileName = Path.GetFileName(targetAssemblyPath);
            var targetAssemblyFileNameWithoutExtension = Path.GetFileNameWithoutExtension(targetAssemblyPath);
            var outputAssemblyPath = args[1];

            var assembly = AssemblyDefinition.ReadAssembly(targetAssemblyPath);
            foreach (var type in assembly.GetTypesDefined<EqualityTargetAttribute>())
            {
                var properties = type.GetPropertiesDefined<EqualityTargetAttribute>().ToArray();
                DefineMethodEquals(type, properties);
                DefineMethodGetHashCode(type, properties);
            }

            assembly.Write(outputAssemblyPath);

            return 0;
        }

        static void DefineMethodEquals(TypeDefinition type, PropertyDefinition[] properties)
        {
            if (properties.Length == 0) return;

            var methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
            var method = new MethodDefinition("Equals", methodAttributes, type.Module.Import(typeof(Boolean)));
            type.Methods.Add(method);
            var obj = new ParameterDefinition(type.Module.Import(typeof(Object)));
            method.Parameters.Add(obj);
            obj.Name = "obj";
            obj.Attributes = ParameterAttributes.None;

            var gen = method.Body.GetILProcessor();
            gen.Body.InitLocals = true;
            var that = new VariableDefinition(type);                                    // 0
            method.Body.Variables.Add(that);
            var areEqual = new VariableDefinition(type.Module.Import(typeof(Boolean))); // 1
            method.Body.Variables.Add(areEqual);
            var isThat = new VariableDefinition(type.Module.Import(typeof(Boolean)));   // 2
            method.Body.Variables.Add(isThat);
            var labelIsThat = Instruction.Create(OpCodes.Nop);
            var labelReturn = Instruction.Create(OpCodes.Nop);
            var labelAreNotEqual = Instruction.Create(OpCodes.Nop);
            var labelStAreEqual = Instruction.Create(OpCodes.Nop);

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
            gen.Append(labelIsThat);

            // return this.Property1 == that.Property1 &&
            //        this.Property2 == that.Property2 &&
            //        this.Property3 == that.Property3 && ...
            if (properties.Length == 1)
            {
                var gm = properties[0].GetMethod;
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
                    gen.Emit(OpCodes.Call, type.Module.Import(opEquality));
                    gen.Emit(OpCodes.Brfalse_S, labelAreNotEqual);
                }
                gen.Emit(OpCodes.Ldc_I4_1);
                gen.Emit(OpCodes.Br_S, labelReturn);
                gen.Append(labelAreNotEqual);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Append(labelReturn);
                gen.Emit(OpCodes.Ret);
            }
            else if (2 <= properties.Length)
            {
                var gm = default(MethodDefinition);
                var opEquality = default(MethodReference);
                gm = properties[0].GetMethod;
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                gen.Emit(OpCodes.Ldloc_0);
                gen.Emit(OpCodes.Callvirt, gm);
                if (!gm.ReturnType.HasOptimizedOpEquality())
                {
                    opEquality = gm.ReturnType.GetMethodOpEquality();
                    gen.Emit(OpCodes.Call, type.Module.Import(opEquality));
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

                    gm = properties[i].GetMethod;
                    opEquality = gm.ReturnType.GetMethodOpEquality();
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Ldloc_0);
                    gen.Emit(OpCodes.Callvirt, gm);
                    if (!gm.ReturnType.HasOptimizedOpEquality())
                    {
                        opEquality = gm.ReturnType.GetMethodOpEquality();
                        gen.Emit(OpCodes.Call, type.Module.Import(opEquality));
                    }
                }
                gen.Emit(OpCodes.Br_S, labelStAreEqual);
                gen.Append(labelAreNotEqual);
                gen.Emit(OpCodes.Ldc_I4_0);
                gen.Append(labelStAreEqual);
                gen.Emit(OpCodes.Stloc_1);
                gen.Emit(OpCodes.Br_S, labelReturn);
                gen.Append(labelReturn);
                gen.Emit(OpCodes.Ldloc_1);
                gen.Emit(OpCodes.Ret);
            }
        }

        static void DefineMethodGetHashCode(TypeDefinition type, PropertyDefinition[] properties)
        {
            if (properties.Length == 0) return;

            var methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig;
            var method = new MethodDefinition("GetHashCode", methodAttributes, type.Module.Import(typeof(Int32)));
            type.Methods.Add(method);

            var getHashCode = type.Module.Import(type.Module.Import(
                typeof(Object)).Resolve().Methods.FirstOrDefault(m => m.Name == "GetHashCode"));

            method.Body.InitLocals = true;
            var gen = method.Body.GetILProcessor();
            if (properties.Length == 1)
            {
                var gm = properties[0].GetMethod;
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                if (gm.ReturnType.IsValueType)
                {
                    var local = new VariableDefinition(gm.ReturnType);
                    method.Body.Variables.Add(local);
                    gen.Emit(OpCodes.Stloc, local);
                    gen.Emit(OpCodes.Ldloca_S, local);
                    gen.Emit(OpCodes.Constrained, gm.ReturnType);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                }
                else
                {
                    var labelNullIfClass = Instruction.Create(OpCodes.Nop);
                    var labelNotNullIfClass = Instruction.Create(OpCodes.Nop);
                    gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                    gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                    gen.Append(labelNullIfClass);
                    gen.Emit(OpCodes.Ldc_I4_0);
                    gen.Append(labelNotNullIfClass);
                }
                gen.Emit(OpCodes.Ret);
            }
            else if (2 <= properties.Length)
            {
                var gm = default(MethodDefinition);
                var labelNullIfClass = default(Instruction);
                var labelNotNullIfClass = default(Instruction);

                gm = properties[0].GetMethod;
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Call, gm);
                if (gm.ReturnType.IsValueType)
                {
                    var local = new VariableDefinition(gm.ReturnType);
                    method.Body.Variables.Add(local);
                    gen.Emit(OpCodes.Stloc, local);
                    gen.Emit(OpCodes.Ldloca_S, local);
                    gen.Emit(OpCodes.Constrained, gm.ReturnType);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                }
                else
                {
                    labelNullIfClass = Instruction.Create(OpCodes.Nop);
                    labelNotNullIfClass = Instruction.Create(OpCodes.Nop);
                    gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    gen.Emit(OpCodes.Callvirt, getHashCode);
                    gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                    gen.Append(labelNullIfClass);
                    gen.Emit(OpCodes.Ldc_I4_0);
                    gen.Append(labelNotNullIfClass);
                }

                for (int i = 1; i < properties.Length; i++)
                {
                    gm = properties[i].GetMethod;
                    gen.Emit(OpCodes.Ldarg_0);
                    gen.Emit(OpCodes.Call, gm);
                    if (gm.ReturnType.IsValueType)
                    {
                        var local = new VariableDefinition(gm.ReturnType);
                        method.Body.Variables.Add(local);
                        gen.Emit(OpCodes.Stloc, local);
                        gen.Emit(OpCodes.Ldloca_S, local);
                        gen.Emit(OpCodes.Constrained, gm.ReturnType);
                        gen.Emit(OpCodes.Callvirt, getHashCode);
                    }
                    else
                    {
                        labelNullIfClass = Instruction.Create(OpCodes.Nop);
                        labelNotNullIfClass = Instruction.Create(OpCodes.Nop);
                        gen.Emit(OpCodes.Brfalse_S, labelNullIfClass);
                        gen.Emit(OpCodes.Ldarg_0);
                        gen.Emit(OpCodes.Call, gm);
                        gen.Emit(OpCodes.Callvirt, getHashCode);
                        gen.Emit(OpCodes.Br_S, labelNotNullIfClass);
                        gen.Append(labelNullIfClass);
                        gen.Emit(OpCodes.Ldc_I4_0);
                        gen.Append(labelNotNullIfClass);
                    }
                    gen.Emit(OpCodes.Xor);
                }
                gen.Emit(OpCodes.Ret);
            }
        }
    }
}
