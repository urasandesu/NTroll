using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class VariableFormula : Formula
    {
        public VariableFormula()
            : base()
        {
            NodeType = NodeType.Variable;
            VariableName = default(string);
        }

        string variableName;
        public string VariableName 
        { 
            get { return variableName; } 
            set { variableName = CheckCanModify(value); } 
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
            sb.Append("\"VariableName\": ");
            AppendValueTo(VariableName, sb);
            sb.Append("}");
        }
    }
}

