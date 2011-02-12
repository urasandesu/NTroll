using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IConditionalFormulaRef : IConditionalFormula, IFormulaRef
    {
        new IFormulaRef Test { get; set; }
        new IFormulaRef IfTrue { get; set; }
        new IFormulaRef IfFalse { get; set; }
    }
}
