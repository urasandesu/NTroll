using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class PropertyFormula : MemberFormula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.Property;
            Member = default(IPropertyDeclaration);
        }

        public const string NameOfMember = "Member";
        IPropertyDeclaration member;
        public new IPropertyDeclaration Member 
        { 
            get { return member; } 
            set 
            {
                SetValue(NameOfMember, value, ref member);
                base.Member = value;
            }
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override void PinCore()
        {
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfMember);
            sb.Append("\": ");
            AppendValueTo(Member, sb);
        }
    }
}

