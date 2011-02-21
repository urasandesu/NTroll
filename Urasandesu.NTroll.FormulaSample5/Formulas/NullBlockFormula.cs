using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class NullBlockFormula : BlockFormula
    {
        public NullBlockFormula()
            : base(true)
        {
        }

        public override void AppendContentTo(StringBuilder sb)
        {
            sb.Append("null");
        }
    }
}
