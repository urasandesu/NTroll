using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class EqualFormula : BinaryFormula
    {

        protected override void InitializeForCodeGeneration()
        {
            base.InitializeForCodeGeneration();
            NodeType = NodeType.Equal;
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
        }
    }
}

