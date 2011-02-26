using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class MemberFormula : Formula
    {
        public MemberFormula()
            : base()
        {
            NodeType = NodeType.None;
            Instance = default(Formula);
            Member = default(IMemberDeclaration);
        }

        Formula instance;
        public Formula Instance 
        { 
            get { return instance; } 
            set { instance = CheckCanModify(value); } 
        }
        IMemberDeclaration member;
        public IMemberDeclaration Member 
        { 
            get { return member; } 
            set { member = CheckCanModify(value); } 
        }


        protected override Formula PinCore()
        {
            Instance = Formula.Pin(Instance);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"Instance\": ");
            if (Instance == null)
            {
                sb.Append("null");
            }
            else
            {
                Instance.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"Member\": ");
            AppendValueTo(Member, sb);
        }
    }
}

