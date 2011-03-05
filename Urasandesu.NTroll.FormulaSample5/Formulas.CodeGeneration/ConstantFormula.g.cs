using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConstantFormula : Formula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.Constant;
            ConstantValue = default(object);
        }

        public const string NameOfConstantValue = "ConstantValue";
        object constantValue;
        public object ConstantValue 
        { 
            get { return constantValue; } 
            set 
            {
                SetValue(NameOfConstantValue, value, ref constantValue);
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
            sb.Append(NameOfConstantValue);
            sb.Append("\": ");
            AppendValueTo(ConstantValue, sb);
        }
    }
}

