using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace Urasandesu.NTroll.AutoGenerationHolic.DynamicAssemblySample.Investigation
{
    class Program
    {
        static int Main(string[] args)
        {
            AssemblyName assemblyNameUrasandesu_NTroll_AutoGenerationHolic_Targets = 
                new AssemblyName("Urasandesu.NTroll.AutoGenerationHolic.Targets.out");
            AssemblyBuilder assemblyUrasandesu_NTroll_AutoGenerationHolic_Targets = 
                AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyNameUrasandesu_NTroll_AutoGenerationHolic_Targets, AssemblyBuilderAccess.Save);
            ModuleBuilder moduleUrasandesu_NTroll_AutoGenerationHolic_Targets =
                assemblyUrasandesu_NTroll_AutoGenerationHolic_Targets.DefineDynamicModule(
                "Urasandesu.NTroll.AutoGenerationHolic.Targets.out", "Urasandesu.NTroll.AutoGenerationHolic.Targets.out.dll");
            TypeBuilder typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer = 
                BuildUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer(moduleUrasandesu_NTroll_AutoGenerationHolic_Targets);
            FieldBuilder fieldCustomerKey = 
                BuildFieldcustomerKey(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer);
            FieldBuilder fieldFirstName = 
                BuildFieldfirstName(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer);
            FieldBuilder fieldLastName = 
                BuildFieldlastName(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer);
            FieldBuilder fieldContractDate = 
                BuildFieldcontractDate(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer);
            PropertyBuilder propertyCustomerKey = 
                BuildPropertyCustomerKey(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, fieldCustomerKey);
            PropertyBuilder propertyFirstName = 
                BuildPropertyFirstName(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, fieldFirstName);
            PropertyBuilder propertyLastName = 
                BuildPropertyLastName(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, fieldLastName);
            PropertyBuilder propertyContractDate = 
                BuildPropertyContractDate(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, fieldContractDate);
            BuildMethodEquals(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, 
                propertyCustomerKey, propertyFirstName, propertyContractDate);
            BuildMethodGetHashCode(typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer, 
                propertyCustomerKey, propertyFirstName, propertyContractDate);
            typeUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer.CreateType();
            assemblyUrasandesu_NTroll_AutoGenerationHolic_Targets.Save("Urasandesu.NTroll.AutoGenerationHolic.Targets.out.dll");

            return 0;
        }

        public static TypeBuilder BuildUrasandesu_NTroll_AutoGenerationHolic_DynamicAssemblySample_Investigation_Customer(ModuleBuilder module)
        {
            TypeBuilder type = 
                module.DefineType(
                    "Urasandesu.NTroll.AutoGenerationHolic.DynamicAssemblySample.Investigation.Customer", 
                    TypeAttributes.Public, 
                    typeof(Object), 
                    Type.EmptyTypes
                );
            return type;
        }

        public static FieldBuilder BuildFieldcustomerKey(TypeBuilder type)
        {
            FieldBuilder field = 
                type.DefineField(
                    "customerKey", 
                    typeof(Int32), 
                    FieldAttributes.Private
                );
            return field;
        }

        public static FieldBuilder BuildFieldfirstName(TypeBuilder type)
        {
            FieldBuilder field = 
                type.DefineField(
                    "firstName", 
                    typeof(String), 
                    FieldAttributes.Private
                );
            return field;
        }

        public static FieldBuilder BuildFieldlastName(TypeBuilder type)
        {
            FieldBuilder field = 
                type.DefineField(
                    "lastName", 
                    typeof(String), 
                    FieldAttributes.Private
                );
            return field;
        }

        public static FieldBuilder BuildFieldcontractDate(TypeBuilder type)
        {
            FieldBuilder field = 
                type.DefineField(
                    "contractDate", 
                    typeof(DateTime), 
                    FieldAttributes.Private
                );
            return field;
        }

        public static PropertyBuilder BuildPropertyCustomerKey(TypeBuilder type, FieldBuilder field1)
        {
            PropertyBuilder property = 
                type.DefineProperty(
                    "CustomerKey", 
                    PropertyAttributes.None, 
                    typeof(Int32), 
                    Type.EmptyTypes
                );
            property.SetSetMethod(BuildMethodset_CustomerKey(type, field1));
            property.SetGetMethod(BuildMethodget_CustomerKey(type, field1));
            return property;
        }

        public static MethodBuilder BuildMethodset_CustomerKey(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("set_CustomerKey", methodAttributes);
            method.SetReturnType(typeof(void));
            method.SetParameters(typeof(Int32));
            ParameterBuilder value = method.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field1);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodget_CustomerKey(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("get_CustomerKey", methodAttributes);
            method.SetReturnType(typeof(Int32));
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder CS_1_0000 = gen.DeclareLocal(typeof(Int32));
            Label label10 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field1);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label10);
            gen.MarkLabel(label10);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static PropertyBuilder BuildPropertyFirstName(TypeBuilder type, FieldBuilder field1)
        {
            PropertyBuilder property = 
                type.DefineProperty(
                    "FirstName", 
                    PropertyAttributes.None, 
                    typeof(String), 
                    Type.EmptyTypes
                );
            property.SetSetMethod(BuildMethodset_FirstName(type, field1));
            property.SetGetMethod(BuildMethodget_FirstName(type, field1));
            return property;
        }

        public static MethodBuilder BuildMethodset_FirstName(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("set_FirstName", methodAttributes);
            method.SetReturnType(typeof(void));
            method.SetParameters(typeof(String));
            ParameterBuilder value = method.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field1);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodget_FirstName(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("get_FirstName", methodAttributes);
            method.SetReturnType(typeof(String));
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder that = gen.DeclareLocal(typeof(String));
            Label label10 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field1);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label10);
            gen.MarkLabel(label10);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static PropertyBuilder BuildPropertyLastName(TypeBuilder type, FieldBuilder field1)
        {
            PropertyBuilder property = 
                type.DefineProperty(
                    "LastName", 
                    PropertyAttributes.None, 
                    typeof(String), 
                    Type.EmptyTypes
                );
            property.SetSetMethod(BuildMethodset_LastName(type, field1));
            property.SetGetMethod(BuildMethodget_LastName(type, field1));
            return property;
        }

        public static MethodBuilder BuildMethodset_LastName(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("set_LastName", methodAttributes);
            method.SetReturnType(typeof(void));
            method.SetParameters(typeof(String));
            ParameterBuilder value = method.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field1);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodget_LastName(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("get_LastName", methodAttributes);
            method.SetReturnType(typeof(String));
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder str = gen.DeclareLocal(typeof(String));
            Label label10 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field1);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label10);
            gen.MarkLabel(label10);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static PropertyBuilder BuildPropertyContractDate(TypeBuilder type, FieldBuilder field1)
        {
            PropertyBuilder property = 
                type.DefineProperty(
                    "ContractDate", 
                    PropertyAttributes.None, 
                    typeof(DateTime), 
                    Type.EmptyTypes
                );
            property.SetSetMethod(BuildMethodset_ContractDate(type, field1));
            property.SetGetMethod(BuildMethodget_ContractDate(type, field1));
            return property;
        }

        public static MethodBuilder BuildMethodset_ContractDate(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("set_ContractDate", methodAttributes);
            method.SetReturnType(typeof(void));
            method.SetParameters(typeof(DateTime));
            ParameterBuilder value = method.DefineParameter(1, ParameterAttributes.None, "value");
            ILGenerator gen = method.GetILGenerator();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stfld, field1);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodget_ContractDate(TypeBuilder type, FieldBuilder field1)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("get_ContractDate", methodAttributes);
            method.SetReturnType(typeof(DateTime));
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder assemblyNameUrasandesu_NTroll_AutoGenerationHolic_Targets = gen.DeclareLocal(typeof(DateTime));
            Label label10 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldfld, field1);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label10);
            gen.MarkLabel(label10);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodEquals(TypeBuilder type, params PropertyBuilder[] properties)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.Virtual
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("Equals", methodAttributes);
            MethodInfo method1 = properties[0].GetGetMethod();
            MethodInfo method2 = properties[1].GetGetMethod();
            MethodInfo method3 = 
                typeof(String).GetMethod(
                    "op_Equality", 
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, 
                    null, 
                    new Type[] 
                    { 
                        typeof(String), 
                        typeof(String) 
                    }, 
                    null
                );
            MethodInfo method4 = properties[2].GetGetMethod();
            MethodInfo method5 = 
                typeof(DateTime).GetMethod(
                    "op_Equality", 
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, 
                    null, 
                    new Type[] 
                    { 
                        typeof(DateTime), 
                        typeof(DateTime) 
                    }, 
                    null
                );
            method.SetReturnType(typeof(Boolean));
            method.SetParameters(typeof(Object));
            ParameterBuilder obj = method.DefineParameter(1, ParameterAttributes.None, "obj");
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder field = gen.DeclareLocal(typeof(Customer));
            LocalBuilder CS_1_0000 = gen.DeclareLocal(typeof(Boolean));
            LocalBuilder flag = gen.DeclareLocal(typeof(Boolean));
            Label label22 = gen.DefineLabel();
            Label label78 = gen.DefineLabel();
            Label label74 = gen.DefineLabel();
            Label label75 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Isinst, typeof(Customer));
            gen.Emit(OpCodes.Dup);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Ldnull);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Ceq);
            gen.Emit(OpCodes.Stloc_2);
            gen.Emit(OpCodes.Ldloc_2);
            gen.Emit(OpCodes.Brtrue_S, label22);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.Emit(OpCodes.Stloc_1);
            gen.Emit(OpCodes.Br_S, label78);
            gen.MarkLabel(label22);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method1);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Callvirt, method1);
            gen.Emit(OpCodes.Bne_Un_S, label74);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method2);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Callvirt, method2);
            gen.Emit(OpCodes.Call, method3);
            gen.Emit(OpCodes.Brfalse_S, label74);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method4);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Call, method5);
            gen.Emit(OpCodes.Br_S, label75);
            gen.MarkLabel(label74);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.MarkLabel(label75);
            gen.Emit(OpCodes.Stloc_1);
            gen.Emit(OpCodes.Br_S, label78);
            gen.MarkLabel(label78);
            gen.Emit(OpCodes.Ldloc_1);
            gen.Emit(OpCodes.Ret);
            return method;
        }

        public static MethodBuilder BuildMethodGetHashCode(TypeBuilder type, params PropertyBuilder[] properties)
        {
            MethodAttributes methodAttributes =
                  MethodAttributes.Public
                | MethodAttributes.Virtual
                | MethodAttributes.HideBySig;
            MethodBuilder method = type.DefineMethod("GetHashCode", methodAttributes);
            MethodInfo method1 = properties[0].GetGetMethod();
            MethodInfo method2 = 
                typeof(Int32).GetMethod(
                    "GetHashCode", 
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, 
                    null, 
                    Type.EmptyTypes, 
                    null
                );
            MethodInfo method3 = properties[1].GetGetMethod();
            MethodInfo method4 = 
                typeof(Object).GetMethod(
                    "GetHashCode", 
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, 
                    null, 
                    Type.EmptyTypes, 
                    null
                );
            MethodInfo method5 = properties[2].GetGetMethod();
            method.SetReturnType(typeof(Int32));
            ILGenerator gen = method.GetILGenerator();
            LocalBuilder field = gen.DeclareLocal(typeof(Int32));
            LocalBuilder CS_1_0000 = gen.DeclareLocal(typeof(Int32));
            LocalBuilder time = gen.DeclareLocal(typeof(DateTime));
            Label label36 = gen.DefineLabel();
            Label label37 = gen.DefineLabel();
            Label label62 = gen.DefineLabel();
            gen.Emit(OpCodes.Nop);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method1);
            gen.Emit(OpCodes.Stloc_1);
            gen.Emit(OpCodes.Ldloca_S, 1);
            gen.Emit(OpCodes.Call, method2);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method3);
            gen.Emit(OpCodes.Brfalse_S, label36);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method3);
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Br_S, label37);
            gen.MarkLabel(label36);
            gen.Emit(OpCodes.Ldc_I4_0);
            gen.MarkLabel(label37);
            gen.Emit(OpCodes.Xor);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Call, method5);
            gen.Emit(OpCodes.Stloc_2);
            gen.Emit(OpCodes.Ldloca_S, 2);
            gen.Emit(OpCodes.Constrained, typeof(DateTime));
            gen.Emit(OpCodes.Callvirt, method4);
            gen.Emit(OpCodes.Xor);
            gen.Emit(OpCodes.Stloc_0);
            gen.Emit(OpCodes.Br_S, label62);
            gen.MarkLabel(label62);
            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Ret);
            return method;
        }
    }
}
