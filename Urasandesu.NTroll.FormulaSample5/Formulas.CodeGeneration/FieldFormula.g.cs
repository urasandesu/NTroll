using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class FieldFormula : MemberFormula
    {
        public FieldFormula()
            : base()
        {
            NodeType = NodeType.Field;
            Member = default(IFieldDeclaration);
        }

		public const string NameOfMember = "Member";
        IFieldDeclaration member;
        public new IFieldDeclaration Member 
        { 
            get { return member; } 
            set { member = CheckCanModify(value); base.Member = value; OnPropertyChanged(NameOfMember); } 
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
            sb.Append("\"");
            sb.Append(NameOfMember);
            sb.Append("\": ");
            AppendValueTo(Member, sb);
            sb.Append("}");
        }
    }
}

