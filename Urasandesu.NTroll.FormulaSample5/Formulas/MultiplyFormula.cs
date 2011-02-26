using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class MultiplyFormula : BinaryFormula
    {
        public override string MethodToStringValueIfDefault
        {
            get { return "\"*\""; }
        }
    }
}
