using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IConditionalFormula : IFormula
    {
        IFormula Test { get; }
        IFormula IfTrue { get; }
        IFormula IfFalse { get; }
    }
}
