using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IReturnFormula : IFormula
    {
        IFormula Object { get; }
    }
}
