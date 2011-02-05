using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IFormulaRef : IFormula
    {
        new NodeType NodeType { get; set; }
        new ITypeDeclaration Type { get; set; }
        new IFormulaRef Parent { get; set; }
        IFormula Establish();
    }
}
