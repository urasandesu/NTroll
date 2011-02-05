using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IBinaryFormulaRef : IBinaryFormula, IFormulaRef
    {
        new IFormulaRef Left { get; set; }
        new IMethodDeclaration Method { get; set; }
        new IFormulaRef Right { get; set; }
        new IBinaryFormula Establish();
    }
}
