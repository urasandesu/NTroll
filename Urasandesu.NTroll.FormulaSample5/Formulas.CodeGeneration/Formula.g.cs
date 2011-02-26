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

		public const string NameOfReferrer = "Referrer";
        Formula referrer;
        public Formula Referrer 
        { 
            get { return referrer; } 
            set { referrer = CheckCanModify(value); OnPropertyChanged(NameOfReferrer); } 
        }
		public const string NameOfNodeType = "NodeType";
        NodeType nodeType;
        public NodeType NodeType 
        { 
            get { return nodeType; } 
            set { nodeType = CheckCanModify(value); OnPropertyChanged(NameOfNodeType); } 
        }
		public const string NameOfTypeDeclaration = "TypeDeclaration";
        ITypeDeclaration typeDeclaration;
        public ITypeDeclaration TypeDeclaration 
        { 
            get { return typeDeclaration; } 
            set { typeDeclaration = CheckCanModify(value); OnPropertyChanged(NameOfTypeDeclaration); } 
        }


        protected virtual Formula PinCore()
        {
            Referrer = Formula.Pin(Referrer);
            return this;
        }


        public virtual void AppendTo(StringBuilder sb)
        {
            sb.Append("\"");
            sb.Append(NameOfNodeType);
            sb.Append("\": ");
            AppendValueTo(NodeType, sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfTypeDeclaration);
            sb.Append("\": ");
            AppendValueTo(TypeDeclaration, sb);
        }
    }
}

