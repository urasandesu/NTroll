using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IConditionalFormula : IFormula
    {
        IFormula Test { get; }
        IFormula IfTrue { get; }
        IFormula IfFalse { get; }
    }
}
