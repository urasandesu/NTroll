using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class Formula 
    {
        public Formula()
            : base()
        {
            Referrer = default(Formula);
            NodeType = NodeType.None;
            TypeDeclaration = default(ITypeDeclaration);
        }

        Formula referrer;
        public Formula Referrer 
        { 
            get { return referrer; } 
            set { referrer = CheckCanModify(value); } 
        }
        NodeType nodeType;
        public NodeType NodeType 
        { 
            get { return nodeType; } 
            set { nodeType = CheckCanModify(value); } 
        }
        ITypeDeclaration typeDeclaration;
        public ITypeDeclaration TypeDeclaration 
        { 
            get { return typeDeclaration; } 
            set { typeDeclaration = CheckCanModify(value); } 
        }


        protected virtual Formula PinCore()
        {
            Referrer = Formula.Pin(Referrer);
            return this;
        }


        public virtual void AppendTo(StringBuilder sb)
        {
            sb.Append("\"NodeType\": ");
            AppendValueTo(NodeType, sb);
            sb.Append(", ");
            sb.Append("\"TypeDeclaration\": ");
            AppendValueTo(TypeDeclaration, sb);
        }
    }
}

