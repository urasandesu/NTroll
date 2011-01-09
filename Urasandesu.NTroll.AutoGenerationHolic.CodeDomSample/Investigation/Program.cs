using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.CSharp;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Collections;

namespace Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample.Investigation
{
    class Program
    {
        static int Main(string[] args)
        {
            // CodeDom Compile Unit
            CodeCompileUnit _compileunit1 = new CodeCompileUnit();


            //
            // Namespace Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample.Investigation
            //
            CodeNamespace _Urasandesu_NTroll_AutoGenerationHolic_CodeDomSample_Investigation_namespace1 = 
                new CodeNamespace("Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample.Investigation");

            // Imports
            _Urasandesu_NTroll_AutoGenerationHolic_CodeDomSample_Investigation_namespace1.Imports.Add(new CodeNamespaceImport("System"));
            _Urasandesu_NTroll_AutoGenerationHolic_CodeDomSample_Investigation_namespace1.Imports.Add(
                new CodeNamespaceImport("Urasandesu.NTroll.AutoGenerationHolic.Helpers"));

            //
            //
            // class Customer
            //
            CodeTypeDeclaration _Customer_class1 = new CodeTypeDeclaration("Customer");
            _Customer_class1.Attributes = MemberAttributes.Final | 
                                          MemberAttributes.Assembly | 
                                          MemberAttributes.FamilyOrAssembly | 
                                          MemberAttributes.Private;
            _Customer_class1.IsClass = true;

            //
            // Field customerKey
            //
            CodeMemberField _customerKey_field1 = new CodeMemberField();
            _customerKey_field1.Attributes = MemberAttributes.Private;
            _customerKey_field1.Name = "customerKey";
            CodeTypeReference _System_Int32_type1 = new CodeTypeReference("System.Int32");
            _customerKey_field1.Type = _System_Int32_type1;
            _Customer_class1.Members.Add(_customerKey_field1);


            //
            // Field firstName
            //
            CodeMemberField _firstName_field1 = new CodeMemberField();
            _firstName_field1.Attributes = MemberAttributes.Private;
            _firstName_field1.Name = "firstName";
            CodeTypeReference _System_String_type1 = new CodeTypeReference("System.String");
            _firstName_field1.Type = _System_String_type1;
            _Customer_class1.Members.Add(_firstName_field1);


            //
            // Field lastName
            //
            CodeMemberField _lastName_field1 = new CodeMemberField();
            _lastName_field1.Attributes = MemberAttributes.Private;
            _lastName_field1.Name = "lastName";
            CodeTypeReference _System_String_type2 = new CodeTypeReference("System.String");
            _lastName_field1.Type = _System_String_type2;
            _Customer_class1.Members.Add(_lastName_field1);


            //
            // Field contractDate
            //
            CodeMemberField _contractDate_field1 = new CodeMemberField();
            _contractDate_field1.Attributes = MemberAttributes.Private;
            _contractDate_field1.Name = "contractDate";
            CodeTypeReference _DateTime_type1 = new CodeTypeReference("DateTime");
            _contractDate_field1.Type = _DateTime_type1;
            _Customer_class1.Members.Add(_contractDate_field1);


            //
            // Property CustomerKey
            //
            CodeMemberProperty _CustomerKey_property1 = new CodeMemberProperty();
            _CustomerKey_property1.Attributes = MemberAttributes.FamilyAndAssembly | 
                                                MemberAttributes.FamilyOrAssembly | 
                                                MemberAttributes.Public;
            _CustomerKey_property1.Name = "CustomerKey";
            CodeTypeReference _System_Int32_type2 = new CodeTypeReference("System.Int32");
            _CustomerKey_property1.Type = _System_Int32_type2;
            _CustomerKey_property1.HasGet = true;
            CodeMethodReturnStatement _return1 = new CodeMethodReturnStatement();
            CodeFieldReferenceExpression _field1 = new CodeFieldReferenceExpression();
            _field1.FieldName = "customerKey";
            CodeThisReferenceExpression _this1 = new CodeThisReferenceExpression();
            _field1.TargetObject = _this1;
            _return1.Expression = _field1;
            _CustomerKey_property1.GetStatements.Add(_return1);

            _CustomerKey_property1.HasSet = true;
            CodeAssignStatement _assign1 = new CodeAssignStatement();
            CodeFieldReferenceExpression _field2 = new CodeFieldReferenceExpression();
            _field2.FieldName = "customerKey";
            CodeThisReferenceExpression _this2 = new CodeThisReferenceExpression();
            _field2.TargetObject = _this2;
            _assign1.Left = _field2;
            CodeVariableReferenceExpression _arg1 = new CodeVariableReferenceExpression();
            _arg1.VariableName = "value";
            _assign1.Right = _arg1;
            _CustomerKey_property1.SetStatements.Add(_assign1);

            _Customer_class1.Members.Add(_CustomerKey_property1);


            //
            // Property FirstName
            //
            CodeMemberProperty _FirstName_property1 = new CodeMemberProperty();
            _FirstName_property1.Attributes = MemberAttributes.FamilyAndAssembly | 
                                              MemberAttributes.FamilyOrAssembly | 
                                              MemberAttributes.Public;
            _FirstName_property1.Name = "FirstName";
            CodeTypeReference _System_String_type3 = new CodeTypeReference("System.String");
            _FirstName_property1.Type = _System_String_type3;
            _FirstName_property1.HasGet = true;
            CodeMethodReturnStatement _return2 = new CodeMethodReturnStatement();
            CodeFieldReferenceExpression _field3 = new CodeFieldReferenceExpression();
            _field3.FieldName = "firstName";
            CodeThisReferenceExpression _this3 = new CodeThisReferenceExpression();
            _field3.TargetObject = _this3;
            _return2.Expression = _field3;
            _FirstName_property1.GetStatements.Add(_return2);

            _FirstName_property1.HasSet = true;
            CodeAssignStatement _assign2 = new CodeAssignStatement();
            CodeFieldReferenceExpression _field4 = new CodeFieldReferenceExpression();
            _field4.FieldName = "firstName";
            CodeThisReferenceExpression _this4 = new CodeThisReferenceExpression();
            _field4.TargetObject = _this4;
            _assign2.Left = _field4;
            CodeVariableReferenceExpression _arg2 = new CodeVariableReferenceExpression();
            _arg2.VariableName = "value";
            _assign2.Right = _arg2;
            _FirstName_property1.SetStatements.Add(_assign2);

            _Customer_class1.Members.Add(_FirstName_property1);


            //
            // Property LastName
            //
            CodeMemberProperty _LastName_property1 = new CodeMemberProperty();
            _LastName_property1.Attributes = MemberAttributes.FamilyAndAssembly | 
                                             MemberAttributes.FamilyOrAssembly | 
                                             MemberAttributes.Public;
            _LastName_property1.Name = "LastName";
            CodeTypeReference _System_String_type4 = new CodeTypeReference("System.String");
            _LastName_property1.Type = _System_String_type4;
            _LastName_property1.HasGet = true;
            CodeMethodReturnStatement _return3 = new CodeMethodReturnStatement();
            CodeFieldReferenceExpression _field5 = new CodeFieldReferenceExpression();
            _field5.FieldName = "lastName";
            CodeThisReferenceExpression _this5 = new CodeThisReferenceExpression();
            _field5.TargetObject = _this5;
            _return3.Expression = _field5;
            _LastName_property1.GetStatements.Add(_return3);

            _LastName_property1.HasSet = true;
            CodeAssignStatement _assign3 = new CodeAssignStatement();
            CodeFieldReferenceExpression _field6 = new CodeFieldReferenceExpression();
            _field6.FieldName = "lastName";
            CodeThisReferenceExpression _this6 = new CodeThisReferenceExpression();
            _field6.TargetObject = _this6;
            _assign3.Left = _field6;
            CodeVariableReferenceExpression _arg3 = new CodeVariableReferenceExpression();
            _arg3.VariableName = "value";
            _assign3.Right = _arg3;
            _LastName_property1.SetStatements.Add(_assign3);

            _Customer_class1.Members.Add(_LastName_property1);


            //
            // Property ContractDate
            //
            CodeMemberProperty _ContractDate_property1 = new CodeMemberProperty();
            _ContractDate_property1.Attributes = MemberAttributes.FamilyAndAssembly | 
                                                 MemberAttributes.FamilyOrAssembly | 
                                                 MemberAttributes.Public;
            _ContractDate_property1.Name = "ContractDate";
            CodeTypeReference _DateTime_type2 = new CodeTypeReference("DateTime");
            _ContractDate_property1.Type = _DateTime_type2;
            _ContractDate_property1.HasGet = true;
            CodeMethodReturnStatement _return4 = new CodeMethodReturnStatement();
            CodeFieldReferenceExpression _field7 = new CodeFieldReferenceExpression();
            _field7.FieldName = "contractDate";
            CodeThisReferenceExpression _this7 = new CodeThisReferenceExpression();
            _field7.TargetObject = _this7;
            _return4.Expression = _field7;
            _ContractDate_property1.GetStatements.Add(_return4);

            _ContractDate_property1.HasSet = true;
            CodeAssignStatement _assign4 = new CodeAssignStatement();
            CodeFieldReferenceExpression _field8 = new CodeFieldReferenceExpression();
            _field8.FieldName = "contractDate";
            CodeThisReferenceExpression _this8 = new CodeThisReferenceExpression();
            _field8.TargetObject = _this8;
            _assign4.Left = _field8;
            CodeVariableReferenceExpression _arg4 = new CodeVariableReferenceExpression();
            _arg4.VariableName = "value";
            _assign4.Right = _arg4;
            _ContractDate_property1.SetStatements.Add(_assign4);

            _Customer_class1.Members.Add(_ContractDate_property1);


            //
            // Function Equals(System.Object obj)
            //
            CodeMemberMethod _Equals_method1 = new CodeMemberMethod();
            _Equals_method1.Attributes = MemberAttributes.Override | 
                                         MemberAttributes.FamilyAndAssembly | 
                                         MemberAttributes.FamilyOrAssembly | 
                                         MemberAttributes.Public;
            _Equals_method1.Name = "Equals";
            CodeTypeReference _System_Object_type1 = new CodeTypeReference("System.Object");
            CodeParameterDeclarationExpression _obj_arg1 = new CodeParameterDeclarationExpression(_System_Object_type1, "obj");
            _obj_arg1.Direction = FieldDirection.In;
            _obj_arg1.Name = "obj";
            CodeTypeReference _System_Object_type2 = new CodeTypeReference("System.Object");
            _obj_arg1.Type = _System_Object_type2;
            _Equals_method1.Parameters.Add(_obj_arg1);
            CodeTypeReference _System_Boolean_type1 = new CodeTypeReference("System.Boolean");
            _Equals_method1.ReturnType = _System_Boolean_type1;

            CodeConditionStatement _if1 = new CodeConditionStatement();
            CodeBinaryOperatorExpression _binop1 = new CodeBinaryOperatorExpression();
            CodeVariableReferenceExpression _arg5 = new CodeVariableReferenceExpression();
            _arg5.VariableName = "obj";
            _binop1.Left = _arg5;
            _binop1.Operator = CodeBinaryOperatorType.IdentityEquality;
            CodePrimitiveExpression _value1 = new CodePrimitiveExpression();
            _value1.Value = null;
            _binop1.Right = _value1;
            _if1.Condition = _binop1;
            CodeMethodReturnStatement _return5 = new CodeMethodReturnStatement();
            CodePrimitiveExpression _value2 = new CodePrimitiveExpression();
            _value2.Value = false;
            _return5.Expression = _value2;
            _if1.TrueStatements.Add(_return5);

            _Equals_method1.Statements.Add(_if1);

            CodeConditionStatement _if2 = new CodeConditionStatement();
            CodeBinaryOperatorExpression _binop2 = new CodeBinaryOperatorExpression();
            CodeMethodInvokeExpression _invoke1 = new CodeMethodInvokeExpression();
            CodeMethodInvokeExpression _invoke2 = new CodeMethodInvokeExpression();
            CodeMethodReferenceExpression _GetType_method1 = new CodeMethodReferenceExpression();
            _GetType_method1.MethodName = "GetType";
            CodeVariableReferenceExpression _arg6 = new CodeVariableReferenceExpression();
            _arg6.VariableName = "obj";
            _GetType_method1.TargetObject = _arg6;
            _invoke2.Method = _GetType_method1;
            _invoke1.Parameters.Add(_invoke2);

            CodeMethodReferenceExpression _IsAssignableFrom_method1 = new CodeMethodReferenceExpression();
            _IsAssignableFrom_method1.MethodName = "IsAssignableFrom";
            CodeTypeOfExpression _typeof1 = new CodeTypeOfExpression();
            CodeTypeReference _Customer_type1 = new CodeTypeReference("Customer");
            _typeof1.Type = _Customer_type1;
            _IsAssignableFrom_method1.TargetObject = _typeof1;
            _invoke1.Method = _IsAssignableFrom_method1;
            _binop2.Left = _invoke1;
            _binop2.Operator = CodeBinaryOperatorType.ValueEquality;
            CodePrimitiveExpression _value3 = new CodePrimitiveExpression();
            _value3.Value = false;
            _binop2.Right = _value3;
            _if2.Condition = _binop2;
            CodeMethodReturnStatement _return6 = new CodeMethodReturnStatement();
            CodePrimitiveExpression _value4 = new CodePrimitiveExpression();
            _value4.Value = false;
            _return6.Expression = _value4;
            _if2.TrueStatements.Add(_return6);

            _Equals_method1.Statements.Add(_if2);

            CodeVariableDeclarationStatement _decl1 = new CodeVariableDeclarationStatement();
            CodeCastExpression _cast1 = new CodeCastExpression();
            CodeVariableReferenceExpression _arg7 = new CodeVariableReferenceExpression();
            _arg7.VariableName = "obj";
            _cast1.Expression = _arg7;
            CodeTypeReference _Customer_type2 = new CodeTypeReference("Customer");
            _cast1.TargetType = _Customer_type2;
            _decl1.InitExpression = _cast1;
            _decl1.Name = "that";
            CodeTypeReference _Customer_type3 = new CodeTypeReference("Customer");
            _decl1.Type = _Customer_type3;
            _Equals_method1.Statements.Add(_decl1);

            CodeMethodReturnStatement _return7 = new CodeMethodReturnStatement();
            CodeBinaryOperatorExpression _binop3 = new CodeBinaryOperatorExpression();
            CodeBinaryOperatorExpression _binop4 = new CodeBinaryOperatorExpression();
            CodeBinaryOperatorExpression _binop5 = new CodeBinaryOperatorExpression();
            CodePropertyReferenceExpression _prop1 = new CodePropertyReferenceExpression();
            _prop1.PropertyName = "CustomerKey";
            CodeThisReferenceExpression _this9 = new CodeThisReferenceExpression();
            _prop1.TargetObject = _this9;
            _binop5.Left = _prop1;
            _binop5.Operator = CodeBinaryOperatorType.IdentityEquality;
            CodePropertyReferenceExpression _prop2 = new CodePropertyReferenceExpression();
            _prop2.PropertyName = "CustomerKey";
            CodeVariableReferenceExpression _arg8 = new CodeVariableReferenceExpression();
            _arg8.VariableName = "that";
            _prop2.TargetObject = _arg8;
            _binop5.Right = _prop2;
            _binop4.Left = _binop5;
            _binop4.Operator = CodeBinaryOperatorType.BooleanAnd;
            CodeBinaryOperatorExpression _binop6 = new CodeBinaryOperatorExpression();
            CodePropertyReferenceExpression _prop3 = new CodePropertyReferenceExpression();
            _prop3.PropertyName = "FirstName";
            CodeThisReferenceExpression _this10 = new CodeThisReferenceExpression();
            _prop3.TargetObject = _this10;
            _binop6.Left = _prop3;
            _binop6.Operator = CodeBinaryOperatorType.IdentityEquality;
            CodePropertyReferenceExpression _prop4 = new CodePropertyReferenceExpression();
            _prop4.PropertyName = "FirstName";
            CodeVariableReferenceExpression _arg9 = new CodeVariableReferenceExpression();
            _arg9.VariableName = "that";
            _prop4.TargetObject = _arg9;
            _binop6.Right = _prop4;
            _binop4.Right = _binop6;
            _binop3.Left = _binop4;
            _binop3.Operator = CodeBinaryOperatorType.BooleanAnd;
            CodeBinaryOperatorExpression _binop7 = new CodeBinaryOperatorExpression();
            CodePropertyReferenceExpression _prop5 = new CodePropertyReferenceExpression();
            _prop5.PropertyName = "ContractDate";
            CodeThisReferenceExpression _this11 = new CodeThisReferenceExpression();
            _prop5.TargetObject = _this11;
            _binop7.Left = _prop5;
            _binop7.Operator = CodeBinaryOperatorType.IdentityEquality;
            CodePropertyReferenceExpression _prop6 = new CodePropertyReferenceExpression();
            _prop6.PropertyName = "ContractDate";
            CodeVariableReferenceExpression _arg10 = new CodeVariableReferenceExpression();
            _arg10.VariableName = "that";
            _prop6.TargetObject = _arg10;
            _binop7.Right = _prop6;
            _binop3.Right = _binop7;
            _return7.Expression = _binop3;
            _Equals_method1.Statements.Add(_return7);

            _Customer_class1.Members.Add(_Equals_method1);


            //
            // Function GetHashCode()
            //
            CodeMemberMethod _GetHashCode_method1 = new CodeMemberMethod();
            _GetHashCode_method1.Attributes = MemberAttributes.Override | 
                                              MemberAttributes.FamilyAndAssembly | 
                                              MemberAttributes.FamilyOrAssembly | 
                                              MemberAttributes.Public;
            _GetHashCode_method1.Name = "GetHashCode";
            CodeTypeReference _System_Int32_type3 = new CodeTypeReference("System.Int32");
            _GetHashCode_method1.ReturnType = _System_Int32_type3;

            CodeMethodReturnStatement _return8 = new CodeMethodReturnStatement();
            CodeMethodInvokeExpression _invoke3 = new CodeMethodInvokeExpression();
            CodeMethodInvokeExpression _invoke4 = new CodeMethodInvokeExpression();
            CodeMethodInvokeExpression _invoke5 = new CodeMethodInvokeExpression();
            CodePropertyReferenceExpression _prop7 = new CodePropertyReferenceExpression();
            _prop7.PropertyName = "CustomerKey";
            CodeThisReferenceExpression _this12 = new CodeThisReferenceExpression();
            _prop7.TargetObject = _this12;
            _invoke5.Parameters.Add(_prop7);

            CodeMethodReferenceExpression _NullableGetHashCode_method1 = new CodeMethodReferenceExpression();
            _NullableGetHashCode_method1.MethodName = "NullableGetHashCode";
            CodeVariableReferenceExpression _arg11 = new CodeVariableReferenceExpression();
            _arg11.VariableName = "Operators";
            _NullableGetHashCode_method1.TargetObject = _arg11;
            _invoke5.Method = _NullableGetHashCode_method1;
            _invoke4.Parameters.Add(_invoke5);

            CodeMethodInvokeExpression _invoke6 = new CodeMethodInvokeExpression();
            CodePropertyReferenceExpression _prop8 = new CodePropertyReferenceExpression();
            _prop8.PropertyName = "FirstName";
            CodeThisReferenceExpression _this13 = new CodeThisReferenceExpression();
            _prop8.TargetObject = _this13;
            _invoke6.Parameters.Add(_prop8);

            CodeMethodReferenceExpression _NullableGetHashCode_method2 = new CodeMethodReferenceExpression();
            _NullableGetHashCode_method2.MethodName = "NullableGetHashCode";
            CodeVariableReferenceExpression _arg12 = new CodeVariableReferenceExpression();
            _arg12.VariableName = "Operators";
            _NullableGetHashCode_method2.TargetObject = _arg12;
            _invoke6.Method = _NullableGetHashCode_method2;
            _invoke4.Parameters.Add(_invoke6);

            CodeMethodReferenceExpression _Eor_method1 = new CodeMethodReferenceExpression();
            _Eor_method1.MethodName = "Eor";
            CodeVariableReferenceExpression _arg13 = new CodeVariableReferenceExpression();
            _arg13.VariableName = "Operators";
            _Eor_method1.TargetObject = _arg13;
            _invoke4.Method = _Eor_method1;
            _invoke3.Parameters.Add(_invoke4);

            CodeMethodInvokeExpression _invoke7 = new CodeMethodInvokeExpression();
            CodePropertyReferenceExpression _prop9 = new CodePropertyReferenceExpression();
            _prop9.PropertyName = "ContractDate";
            CodeThisReferenceExpression _this14 = new CodeThisReferenceExpression();
            _prop9.TargetObject = _this14;
            _invoke7.Parameters.Add(_prop9);

            CodeMethodReferenceExpression _NullableGetHashCode_method3 = new CodeMethodReferenceExpression();
            _NullableGetHashCode_method3.MethodName = "NullableGetHashCode";
            CodeVariableReferenceExpression _arg14 = new CodeVariableReferenceExpression();
            _arg14.VariableName = "Operators";
            _NullableGetHashCode_method3.TargetObject = _arg14;
            _invoke7.Method = _NullableGetHashCode_method3;
            _invoke3.Parameters.Add(_invoke7);

            CodeMethodReferenceExpression _Eor_method2 = new CodeMethodReferenceExpression();
            _Eor_method2.MethodName = "Eor";
            CodeVariableReferenceExpression _arg15 = new CodeVariableReferenceExpression();
            _arg15.VariableName = "Operators";
            _Eor_method2.TargetObject = _arg15;
            _invoke3.Method = _Eor_method2;
            _return8.Expression = _invoke3;
            _GetHashCode_method1.Statements.Add(_return8);

            _Customer_class1.Members.Add(_GetHashCode_method1);

            _Urasandesu_NTroll_AutoGenerationHolic_CodeDomSample_Investigation_namespace1.Types.Add(_Customer_class1);

            _compileunit1.Namespaces.Add(_Urasandesu_NTroll_AutoGenerationHolic_CodeDomSample_Investigation_namespace1);







            var provider = new CSharpCodeProvider();
            var options = new CompilerParameters();
            options.GenerateInMemory = false;
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("Urasandesu.NTroll.AutoGenerationHolic.Helpers.dll");
            options.OutputAssembly = "Urasandesu.NTroll.AutoGenerationHolic.Targets.out.dll";
            var result = provider.CompileAssemblyFromDom(options, _compileunit1);

            if (0 < result.Errors.Count)
            {
                Console.WriteLine(string.Join("\r\n", result.Errors.Select<CompilerError, string>(error => error.ToString()).ToArray()));
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
