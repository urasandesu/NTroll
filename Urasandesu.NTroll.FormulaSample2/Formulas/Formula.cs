using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public abstract class Formula : IFormula
    {
        public NodeType NodeType { get; protected set; }
        public ITypeDeclaration Type { get; protected set; }
        public IFormula Parent { get; protected set; }
        public abstract IFormula Accept(IFormulaVisitor visitor);

        public sealed override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{");
            FirstHierarchyOnly(() => AppendToString(sb));
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
            sb.Append(", ");
            sb.Append("\"Parent\": ");
            sb.Append(Parent.NullableToString());
        }

        protected Formula(NodeType nodeType, ITypeDeclaration type, IFormula parent)
        {
            NodeType = nodeType;
            Type = type;
            Parent = parent;
        }

        int hierarchy;
        protected void FirstHierarchyOnly(Action action)
        {
            try
            {
                if (hierarchy++ == 0)
                {
                    action();
                }
            }
            finally
            {
                hierarchy--;
            }
        }

        public static IConstantFormula Constant(object value, ITypeDeclaration type, IFormula parent)
        {
            return new ConstantFormula(value, type, parent);
        }

        public static IVariableFormula Variable(ITypeDeclaration type, string name, IFormula parent)
        {
            return new VariableFormula(type, name, parent);
        }

        public static IAssignFormula Assign(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new AssignFormula(left, right, type, method, parent);
        }

        public static IBlockFormula Block(
            IBlockFormula parentBlock,
            ReadOnlyCollection<IBlockFormula> childBlocks,
            ReadOnlyCollection<IFormula> variables,
            ReadOnlyCollection<IFormula> formulas,
            IFormula result,
            ITypeDeclaration type,
            IFormula parent)
        {
            return new BlockFormula(parentBlock, childBlocks, variables, formulas, result, type, parent);
        }

        public static IEqualFormula Equal(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new EqualFormula(left, right, type, method, parent);
        }

        public static IConditionalFormula Condition(IFormula test, IFormula ifTrue, IFormula ifFalse, ITypeDeclaration type, IFormula parent)
        {
            return new ConditionalFormula(test, ifTrue, ifFalse, type, parent);
        }

        public static IMethodCallFormula Call(IFormula instance, IMethodDeclaration method, ReadOnlyCollection<IFormula> arguments, ITypeDeclaration type, IFormula parent)
        {
            return new MethodCallFormula(instance, method, arguments, type, parent);
        }

        public static IConvertFormula Convert(ITypeDeclaration type, IFormula operand, IMethodDeclaration method, IFormula parent)
        {
            return new ConvertFormula(type, operand, method, parent);
        }

        public static IAndAlsoFormula AndAlso(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new AndAlsoFormula(left, right, type, method, parent);
        }

        public static INotEqualFormula NotEqual(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new NotEqualFormula(left, right, type, method, parent);
        }

        public static ITypeAsFormula TypeAs(ITypeDeclaration type, IFormula operand, IMethodDeclaration method, IFormula parent)
        {
            return new TypeAsFormula(type, operand, method, parent);
        }

        public static IReturnFormula Return(IFormula formula, ITypeDeclaration type, IFormula parent)
        {
            return new ReturnFormula(formula, type, parent);
        }

        public static IAddFormula Add(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new AddFormula(left, right, type, method, parent);
        }

        public static IMultiplyFormula Multiply(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new MultiplyFormula(left, right, type, method, parent);
        }

        public static IExclusiveOrFormula ExclusiveOr(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
        {
            return new ExclusiveOrFormula(left, right, type, method, parent);
        }
    }
}
