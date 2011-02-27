using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

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
            Initialize();
        }

        public const string NameOfInstance = "Instance";
        Formula instance;
        public Formula Instance 
        { 
            get { return instance; } 
            set 
            {
                SetFormula(NameOfInstance, value, ref instance);
            }
        }
        public const string NameOfMember = "Member";
        IMemberDeclaration member;
        public IMemberDeclaration Member 
        { 
            get { return member; } 
            set 
            {
                SetValue(NameOfMember, value, ref member);
            }
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
            sb.Append("\"");
            sb.Append(NameOfInstance);
            sb.Append("\": ");
            if (Instance == null)
            {
                sb.Append("null");
            }
            else
            {
                Instance.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfMember);
            sb.Append("\": ");
            AppendValueTo(Member, sb);
        }
    }
}

