using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class NodeTypeRef : TerminalRef<NonterminalType>
    {
        public NodeTypeRef(NonterminalType nodeType)
            : base(INonterminalMixin.NodeTypeName, nodeType, default(INodeRef))
        {
        }

        protected override ITerminal<NonterminalType> Pin(string name, NonterminalType value, INode referrers)
        {
            return new NodeType(value);
        }
    }
}
