using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConstantFormula : Formula
    {
        public ConstantFormula()
            : base()
        {
            NodeType = NodeType.Constant;
            ConstantValue = default(object);
        }

		public const string NameOfConstantValue = "ConstantValue";
        object constantValue;
        public object ConstantValue 
        { 
            get { return constantValue; } 
            set { constantValue = CheckCanModify(value); OnPropertyChanged(NameOfConstantValue); } 
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
            sb.Append(NameOfConstantValue);
            sb.Append("\": ");
            AppendValueTo(ConstantValue, sb);
            sb.Append("}");
        }
    }
}

