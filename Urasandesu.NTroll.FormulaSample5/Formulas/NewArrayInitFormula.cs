using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewArrayInitFormula : Formula
    {
        public NewArrayInitFormula(Formula[] formulas)
            : this()
        {
            formulas.AddRangeTo(Formulas);
        }
    }
}
