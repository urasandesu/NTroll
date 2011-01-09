using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Collections;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System.Reflection;

namespace Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample
{
    class Program
    {
        static int Main(string[] args)
        {
            // CodeDOM 版 Equals メソッド、GetHashCode メソッドを自動生成処理。
            // 指定されたアセンブリを読み取り、EqualityTargetAttribute カスタム属性を持つ型を列挙。
            // その型が持つプロパティを読み取り、EqualityTargetAttribute カスタム属性を持つプロパティに絞込み。
            // 上記のプロパティを使って、Equals メソッド、GetHashCode メソッドを自動生成する。
            // 加えて、中身を再現することはできないので、ルールをあらかじめ決めておく必要がある。
            // ここでは、以下のルールを設けた。
            // - インポートする名前空間は固定。
            // - 参照するアセンブリは固定。
            // - プロパティと対応するフィールドが存在する。フィールド名はプロパティ名の頭文字を小文字化したもの。
            // - フィールドのアクセス修飾子は Private。
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample.exe <TargetAssemblyPath> <OutputAssemblyPath>");
                return -1;
            }

            var targetAssemblyPath = args[0];
            var outputAssemblyPath = args[1];

            var compileUnit = new CodeCompileUnit();
            foreach (var t in Assembly.LoadFrom(targetAssemblyPath).GetTypesDefined<EqualityTargetAttribute>())
            {
                // 名前空間は型のものを利用。
                // インポートする名前空間は固定。
                var @namespace = compileUnit.Namespaces.
                                    FirstOrDefault<CodeNamespace>(_ => _.Name == t.Namespace) ?? NewNamespace(t.Namespace);

                // クラスは型のものを利用。
                var @class = new CodeTypeDeclaration(t.Name);
                @class.Attributes = t.GetMemberAttributes();
                @class.IsClass = true;

                // プロパティと対応するフィールドが存在する。フィールド名はプロパティ名の頭文字を小文字化したもの。
                // フィールドのアクセス修飾子は Private。
                foreach (var propertyInfo in t.GetProperties())
                {
                    // フィールド。
                    {
                        var field = new CodeMemberField();
                        field.Attributes = MemberAttributes.Private;
                        field.Name = propertyInfo.Name.ToCamel();
                        var fieldType = new CodeTypeReference(propertyInfo.PropertyType.FullName);
                        field.Type = fieldType;
                        @class.Members.Add(field);
                    }


                    // プロパティ。
                    {
                        var property = new CodeMemberProperty();
                        property.Attributes = propertyInfo.GetMemberAttributes();
                        property.Name = propertyInfo.Name;
                        var propertyType = new CodeTypeReference(propertyInfo.PropertyType.FullName);
                        property.Type = propertyType;

                        var field = new CodeFieldReferenceExpression();
                        field.FieldName = propertyInfo.Name.ToCamel();
                        var @this = new CodeThisReferenceExpression();
                        field.TargetObject = @this;

                        property.HasGet = true;
                        var @return = new CodeMethodReturnStatement();
                        @return.Expression = field;
                        property.GetStatements.Add(@return);

                        property.HasSet = true;
                        var assign = new CodeAssignStatement();
                        assign.Left = field;
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "value";
                        assign.Right = arg;
                        property.SetStatements.Add(assign);

                        @class.Members.Add(property);
                    }
                }

                // Equals メソッド
                {
                    var equals = new CodeMemberMethod();
                    equals.Attributes = MemberAttributes.Override |
                                        MemberAttributes.FamilyAndAssembly |
                                        MemberAttributes.FamilyOrAssembly |
                                        MemberAttributes.Public;
                    equals.Name = "Equals";
                    var objectType = new CodeTypeReference("System.Object");
                    var objArg = new CodeParameterDeclarationExpression(objectType, "obj");
                    objArg.Direction = FieldDirection.In;
                    objArg.Name = "obj";
                    objArg.Type = objectType;
                    equals.Parameters.Add(objArg);
                    var booleanType = new CodeTypeReference("System.Boolean");
                    equals.ReturnType = booleanType;

                    // if (obj == null) return false;
                    {
                        var @if = new CodeConditionStatement();
                        var binop = new CodeBinaryOperatorExpression();
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "obj";
                        binop.Left = arg;
                        binop.Operator = CodeBinaryOperatorType.IdentityEquality;
                        var valueNull = new CodePrimitiveExpression();
                        valueNull.Value = null;
                        binop.Right = valueNull;
                        @if.Condition = binop;
                        var @return = new CodeMethodReturnStatement();
                        var valueFalse = new CodePrimitiveExpression();
                        valueFalse.Value = false;
                        @return.Expression = valueFalse;
                        @if.TrueStatements.Add(@return);

                        equals.Statements.Add(@if);
                    }

                    // if (!typeof(...).IsAssignableFrom(obj.GetType())) return false;
                    {
                        var @if = new CodeConditionStatement();
                        var binop = new CodeBinaryOperatorExpression();
                        var invoke1 = new CodeMethodInvokeExpression();
                        var invoke2 = new CodeMethodInvokeExpression();
                        var getType = new CodeMethodReferenceExpression();
                        getType.MethodName = "GetType";
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "obj";
                        getType.TargetObject = arg;
                        invoke2.Method = getType;
                        invoke1.Parameters.Add(invoke2);

                        var isAssignableFrom = new CodeMethodReferenceExpression();
                        isAssignableFrom.MethodName = "IsAssignableFrom";
                        var @typeof = new CodeTypeOfExpression();
                        var type = new CodeTypeReference(t.Name);
                        @typeof.Type = type;
                        isAssignableFrom.TargetObject = @typeof;
                        invoke1.Method = isAssignableFrom;
                        binop.Left = invoke1;
                        binop.Operator = CodeBinaryOperatorType.ValueEquality;
                        var valueFalse = new CodePrimitiveExpression();
                        valueFalse.Value = false;
                        binop.Right = valueFalse;
                        @if.Condition = binop;
                        var @return = new CodeMethodReturnStatement();
                        @return.Expression = valueFalse;
                        @if.TrueStatements.Add(@return);

                        equals.Statements.Add(@if);
                    }

                    // ... that = (...)obj;
                    {
                        var decl = new CodeVariableDeclarationStatement();
                        var cast = new CodeCastExpression();
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "obj";
                        cast.Expression = arg;
                        var type = new CodeTypeReference(t.Name);
                        cast.TargetType = type;
                        decl.InitExpression = cast;
                        decl.Name = "that";
                        decl.Type = type;
                        equals.Statements.Add(decl);
                    }

                    // return this.Property1 == that.Property1 &&
                    //        this.Property2 == that.Property2 &&
                    //        this.Property3 == that.Property3 && ...
                    {
                        var @return = new CodeMethodReturnStatement();
                        var properties = t.GetPropertiesDefined<EqualityTargetAttribute>().ToArray();

                        if (properties.Length == 0)
                        {
                            var valueTrue = new CodePrimitiveExpression();
                            valueTrue.Value = true;
                            @return.Expression = valueTrue;
                            equals.Statements.Add(@return);
                        }
                        else if (properties.Length == 1)
                        {
                            var binopEquality = new CodeBinaryOperatorExpression();
                            binopEquality.Operator = CodeBinaryOperatorType.IdentityEquality;
                            {
                                var prop = new CodePropertyReferenceExpression();
                                prop.PropertyName = properties[0].Name;
                                var @this = new CodeThisReferenceExpression();
                                prop.TargetObject = @this;
                                binopEquality.Left = prop;
                            }
                            {
                                var prop = new CodePropertyReferenceExpression();
                                prop.PropertyName = properties[0].Name;
                                var arg = new CodeVariableReferenceExpression();
                                arg.VariableName = "that";
                                prop.TargetObject = arg;
                                binopEquality.Right = prop;
                            }
                            @return.Expression = binopEquality;
                        }
                        else if (2 <= properties.Length)
                        {
                            var _binopAndAlso = new CodeBinaryOperatorExpression();
                            _binopAndAlso.Operator = CodeBinaryOperatorType.BooleanAnd;
                            {
                                var binopEquality = new CodeBinaryOperatorExpression();
                                binopEquality.Operator = CodeBinaryOperatorType.IdentityEquality;
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[0].Name;
                                    var @this = new CodeThisReferenceExpression();
                                    prop.TargetObject = @this;
                                    binopEquality.Left = prop;
                                }
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[0].Name;
                                    var arg = new CodeVariableReferenceExpression();
                                    arg.VariableName = "that";
                                    prop.TargetObject = arg;
                                    binopEquality.Right = prop;
                                }
                                _binopAndAlso.Left = binopEquality;
                            }
                            {
                                var binopEquality = new CodeBinaryOperatorExpression();
                                binopEquality.Operator = CodeBinaryOperatorType.IdentityEquality;
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[1].Name;
                                    var @this = new CodeThisReferenceExpression();
                                    prop.TargetObject = @this;
                                    binopEquality.Left = prop;
                                }
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[1].Name;
                                    var arg = new CodeVariableReferenceExpression();
                                    arg.VariableName = "that";
                                    prop.TargetObject = arg;
                                    binopEquality.Right = prop;
                                }
                                _binopAndAlso.Right = binopEquality;
                            }

                            var binopAndAlso = _binopAndAlso;
                            for (int i = 2; i < properties.Length; i++)
                            {
                                var binop = new CodeBinaryOperatorExpression();
                                binop.Operator = CodeBinaryOperatorType.IdentityEquality;
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[i].Name;
                                    var @this = new CodeThisReferenceExpression();
                                    prop.TargetObject = @this;
                                    binop.Left = prop;
                                }
                                {
                                    var prop = new CodePropertyReferenceExpression();
                                    prop.PropertyName = properties[i].Name;
                                    var arg = new CodeVariableReferenceExpression();
                                    arg.VariableName = "that";
                                    prop.TargetObject = arg;
                                    binop.Right = prop;
                                }
                                binopAndAlso = new CodeBinaryOperatorExpression(binopAndAlso, CodeBinaryOperatorType.BooleanAnd, binop);
                            }
                            @return.Expression = binopAndAlso;
                        }
                        equals.Statements.Add(@return);
                    }

                    @class.Members.Add(equals);
                }


                // GetHashCode メソッド
                {
                    var getHashCode = new CodeMemberMethod();
                    getHashCode.Attributes = MemberAttributes.Override |
                                             MemberAttributes.FamilyAndAssembly |
                                             MemberAttributes.FamilyOrAssembly |
                                             MemberAttributes.Public;
                    getHashCode.Name = "GetHashCode";
                    var intType = new CodeTypeReference("System.Int32");
                    getHashCode.ReturnType = intType;

                    var @return = new CodeMethodReturnStatement();
                    var properties = t.GetPropertiesDefined<EqualityTargetAttribute>().ToArray();

                    if (properties.Length == 0)
                    {
                        var valueZero = new CodePrimitiveExpression();
                        valueZero.Value = 0;
                        @return.Expression = valueZero;
                    }
                    else if (properties.Length == 1)
                    {
                        var nullableGetHashCodeInvoke = new CodeMethodInvokeExpression();
                        var nullableGetHashCode = new CodeMethodReferenceExpression();
                        nullableGetHashCode.MethodName = "NullableGetHashCode";
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "Operators";
                        nullableGetHashCode.TargetObject = arg;
                        nullableGetHashCodeInvoke.Method = nullableGetHashCode;
                        var prop = new CodePropertyReferenceExpression();
                        prop.PropertyName = properties[0].Name;
                        var @this = new CodeThisReferenceExpression();
                        prop.TargetObject = @this;
                        nullableGetHashCodeInvoke.Parameters.Add(prop);
                        @return.Expression = nullableGetHashCodeInvoke;
                    }
                    else if (2 <= properties.Length)
                    {
                        var _eorInvoke = new CodeMethodInvokeExpression();
                        var eor = new CodeMethodReferenceExpression();
                        eor.MethodName = "Eor";
                        var arg = new CodeVariableReferenceExpression();
                        arg.VariableName = "Operators";
                        eor.TargetObject = arg;
                        _eorInvoke.Method = eor;
                        {
                            var nullableGetHashCodeInvoke = new CodeMethodInvokeExpression();
                            var nullableGetHashCode = new CodeMethodReferenceExpression();
                            nullableGetHashCode.MethodName = "NullableGetHashCode";
                            nullableGetHashCode.TargetObject = arg;
                            nullableGetHashCodeInvoke.Method = nullableGetHashCode;
                            var prop = new CodePropertyReferenceExpression();
                            prop.PropertyName = properties[0].Name;
                            var @this = new CodeThisReferenceExpression();
                            prop.TargetObject = @this;
                            nullableGetHashCodeInvoke.Parameters.Add(prop);
                            _eorInvoke.Parameters.Add(nullableGetHashCodeInvoke);
                        }
                        {
                            var nullableGetHashCodeInvoke = new CodeMethodInvokeExpression();
                            var nullableGetHashCode = new CodeMethodReferenceExpression();
                            nullableGetHashCode.MethodName = "NullableGetHashCode";
                            nullableGetHashCode.TargetObject = arg;
                            nullableGetHashCodeInvoke.Method = nullableGetHashCode;
                            var prop = new CodePropertyReferenceExpression();
                            prop.PropertyName = properties[1].Name;
                            var @this = new CodeThisReferenceExpression();
                            prop.TargetObject = @this;
                            nullableGetHashCodeInvoke.Parameters.Add(prop);
                            _eorInvoke.Parameters.Add(nullableGetHashCodeInvoke);
                        }

                        var eorInvoke = _eorInvoke;
                        for (int i = 2; i < properties.Length; i++)
                        {
                            var nullableGetHashCodeInvoke = new CodeMethodInvokeExpression();
                            var nullableGetHashCode = new CodeMethodReferenceExpression();
                            nullableGetHashCode.MethodName = "NullableGetHashCode";
                            nullableGetHashCode.TargetObject = arg;
                            nullableGetHashCodeInvoke.Method = nullableGetHashCode;
                            var prop = new CodePropertyReferenceExpression();
                            prop.PropertyName = properties[i].Name;
                            var @this = new CodeThisReferenceExpression();
                            prop.TargetObject = @this;
                            nullableGetHashCodeInvoke.Parameters.Add(prop);
                            eorInvoke = new CodeMethodInvokeExpression(eor, eorInvoke, nullableGetHashCodeInvoke);
                        }
                        @return.Expression = eorInvoke;
                    }
                    getHashCode.Statements.Add(@return);

                    @class.Members.Add(getHashCode);
                }

                @namespace.Types.Add(@class);

                compileUnit.Namespaces.Add(@namespace);
            }






            var provider = new CSharpCodeProvider();
            var options = new CompilerParameters();
            options.GenerateInMemory = false;
            options.ReferencedAssemblies.Add("System.dll");
            options.ReferencedAssemblies.Add("Urasandesu.NTroll.AutoGenerationHolic.Helpers.dll");
            options.OutputAssembly = outputAssemblyPath;
            var result = provider.CompileAssemblyFromDom(options, compileUnit);

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

        static CodeNamespace NewNamespace(string name)
        {
            var @namespace = new CodeNamespace(name);
            @namespace.Imports.Add(new CodeNamespaceImport("System"));
            @namespace.Imports.Add(new CodeNamespaceImport("Urasandesu.NTroll.AutoGenerationHolic.Helpers"));
            return @namespace;
        }
    }
}
