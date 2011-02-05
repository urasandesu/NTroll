using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface ITypeAsFormulaRef : ITypeAsFormula, IUnaryFormulaRef
    {
        new ITypeAsFormula Establish();
    }
}
