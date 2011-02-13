using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface INonterminalRef : INonterminal, INodeRef
    {
        new ITerminalRef<NonterminalType> NodeType { get; set; }
        new ITerminalRef<ITypeDeclaration> Type { get; set; }
        new INonterminal Pin();
    }
}
