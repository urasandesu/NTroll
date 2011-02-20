using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class NullFormula : Formula
    {
        public NullFormula()
            : base(true)
        {
        }

        public override void AppendContentTo(StringBuilder sb)
        {
            sb.Append("null");
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            return null;
        }
    }
}
