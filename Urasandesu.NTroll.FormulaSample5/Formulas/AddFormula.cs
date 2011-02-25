using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class AddFormula : BinaryFormula
    {
        public override string MethodDefaultExpandString
        {
            get { return "\"+\""; }
        }
    }
}
