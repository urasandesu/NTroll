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

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.Variable;
            VariableName = default(string);
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


        protected override void PinCore()
        {
            base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfVariableName);
            sb.Append("\": ");
            AppendValueTo(VariableName, sb);
        }
    }
}

