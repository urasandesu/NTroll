using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IUnaryFormulaRef : IUnaryFormula, IFormulaRef
    {
        new IMethodDeclaration Method { get; set; }
        new IFormulaRef Operand { get; set; }
        new IUnaryFormula Establish();
    }
}
