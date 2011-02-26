using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class PropertyFormula : MemberFormula
    {
        public PropertyFormula()
            : base()
        {
            NodeType = NodeType.Property;
            Member = default(IPropertyDeclaration);
        }

        IPropertyDeclaration member;
        public new IPropertyDeclaration Member 
        { 
            get { return member; } 
            set { member = CheckCanModify(value); base.Member = value; } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Member\": ");
            AppendValueTo(Member, sb);
            sb.Append("}");
        }
    }
}

