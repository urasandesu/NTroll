using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public abstract class Formula
    {
        public NodeType NodeType { get; private set; }
        public ITypeDeclaration Type { get; private set; }

        protected Formula(NodeType nodeType, ITypeDeclaration type)
        {
            NodeType = nodeType;
            Type = type;
        }

        public static MethodCallFormula Call(Formula instance, MethodInfo method, ReadOnlyCollection<Formula> arguments)
        {
            return Call(instance, method.ToMethodDecl(), arguments);
        }

        public static MethodCallFormula Call(Formula instance, IMethodDeclaration method, ReadOnlyCollection<Formula> arguments)
        {
            return new MethodCallFormula(instance, method, arguments);
        }

        public static MethodCallFormula Call(Formula instance, MethodInfo method, IEnumerable<Formula> arguments)
        {
            return Call(instance, method.ToMethodDecl(), new ReadOnlyCollection<Formula>(arguments.ToArray()));
        }

        public static MethodCallFormula Call(Formula instance, IMethodDeclaration method, IEnumerable<Formula> arguments)
        {
            return Call(instance, method, new ReadOnlyCollection<Formula>(arguments.ToArray()));
        }

        public static NewFormula New(ConstructorInfo constructor, ReadOnlyCollection<Formula> arguments)
        {
            return New(constructor.ToConstructorDecl(), arguments);
        }

        public static NewFormula New(IConstructorDeclaration constructor, ReadOnlyCollection<Formula> arguments)
        {
            return new NewFormula(constructor, arguments, null);
        }

        public static AssignFormula Assign(Formula left, Formula right)
        {
            return new AssignFormula(left, null, right);
        }

        public static VariableFormula Variable(Type type, string name)
        {
            return Variable(type.ToTypeDecl(), name);
        }

        public static VariableFormula Variable(ITypeDeclaration type, string name)
        {
            return new VariableFormula(type, name);
        }

        public static FieldFormula Field(Formula instance, FieldInfo field)
        {
            return Field(instance, field.ToFieldDecl());
        }

        public static FieldFormula Field(Formula instance, IFieldDeclaration field)
        {
            return new FieldFormula(instance, field);
        }

        public static ConvertFormula Convert(Formula operand, Type type)
        {
            return Convert(operand, type.ToTypeDecl());
        }

        public static ConvertFormula Convert(Formula operand, ITypeDeclaration type)
        {
            return new ConvertFormula(operand, type);
        }

        public static ConstantFormula Constant(object value)
        {
            return Constant(value, value.GetType());
        }

        public static ConstantFormula Constant(object value, Type type)
        {
            return Constant(value, type.ToTypeDecl());
        }

        public static ConstantFormula Constant(object value, ITypeDeclaration type)
        {
            return new ConstantFormula(value, type);
        }

        public static NewArrayInitFormula NewArrayInit(ReadOnlyCollection<Formula> initializers)
        {
            return new NewArrayInitFormula(initializers);
        }

        public static NewArrayInitFormula NewArrayInit(IEnumerable<Formula> initializers)
        {
            return NewArrayInit(new ReadOnlyCollection<Formula>(initializers.ToArray()));
        }

        public static PropertyFormula Property(Formula instance, PropertyInfo property)
        {
            return Property(instance, property.ToPropertyDecl());
        }

        public static PropertyFormula Property(Formula instance, IPropertyDeclaration property)
        {
            return new PropertyFormula(instance, property);
        }


        public abstract Formula Accept(IFormulaVisitor visitor);

        public sealed override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            AppendToString(sb);
            sb.Append("}");
            return sb.ToString();
        }

        protected virtual void AppendToString(StringBuilder sb)
        {
            sb.Append("\"NodeType\": ");
            sb.Append("\"");
            sb.Append(NodeType);
            sb.Append("\"");
            sb.Append(", ");
            sb.Append("\"Type\": ");
            sb.Append("\"");
            sb.Append(Type.NullableToString());
            sb.Append("\"");
        }
    }

    public class ConditionalFormula : Formula
    {
        protected internal ConditionalFormula(Formula test, Formula ifTrue, Formula ifFalse)
            : base(NodeType.Conditional, ifTrue.Type)
        {
            Test = test;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public Formula Test { get; private set; }
        public Formula IfTrue { get; private set; }
        public Formula IfFalse { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }

    public class BlockFormula : Formula
    {
        protected internal BlockFormula(ReadOnlyCollection<Formula> formulas, Formula result, ReadOnlyCollection<Formula> variables)
            : base(NodeType.Block, result.Type)
        {
            Formulas = formulas;
            Result = result;
            Variables = variables;
        }

        public ReadOnlyCollection<Formula> Formulas { get; private set; }
        public Formula Result { get; private set; }
        public ReadOnlyCollection<Formula> Variables { get; private set; }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
