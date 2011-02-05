using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IConstantFormulaRef : IConstantFormula, IFormulaRef
    {
        new object Value { get; set; }
        new IConstantFormula Establish();
    }
}
