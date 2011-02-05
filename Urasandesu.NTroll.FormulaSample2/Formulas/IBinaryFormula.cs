using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IBinaryFormula : IFormula
    {
        IFormula Left { get; }
        IMethodDeclaration Method { get; }
        IFormula Right { get; }
    }
}
