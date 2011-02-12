using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IFormulaRef : IFormula, INodeRef, IImmutable<IFormula>
    {
        new IConstantNodeRef<NodeType> NodeType { get; set; }
        new IConstantNodeRef<ITypeDeclaration> Type { get; set; }
        new IFormula Pin();
    }
}
