using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class VariableFormula : Formula
    {
        public VariableFormula()
            : base()
        {
            NodeType = NodeType.Variable;
            VariableName = default(string);
            Initialize();
        }

        public const string NameOfVariableName = "VariableName";
        string variableName;
        public string VariableName 
        { 
            get { return variableName; } 
            set 
            {
                SetValue(NameOfVariableName, value, ref variableName);
            }
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
            sb.Append(NameOfVariableName);
            sb.Append("\": ");
            AppendValueTo(VariableName, sb);
            sb.Append("}");
        }
    }
}

