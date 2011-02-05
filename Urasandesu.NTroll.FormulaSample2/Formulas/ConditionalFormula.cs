using System;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConditionalFormula : Formula, IConditionalFormula
    {
        protected internal ConditionalFormula(IFormula test, IFormula ifTrue, IFormula ifFalse, ITypeDeclaration type, IFormula parent)
            : base(NodeType.Conditional, type, parent)
        {
            Test = test;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public IFormula Test { get; protected set; }
        public IFormula IfTrue { get; protected set; }
        public IFormula IfFalse { get; protected set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Test\": ");
            sb.Append(Test.NullableToString());
            sb.Append(", ");
            sb.Append("\"IfTrue\": ");
            sb.Append(IfTrue.NullableToString());
            sb.Append(", ");
            sb.Append("\"IfFalse\": ");
            sb.Append(IfFalse.NullableToString());
        }
    }
}
