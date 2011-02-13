using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class NodeType : Terminal<NonterminalType>
    {
        public NodeType(NonterminalType nodeType)
            : base(INonterminalMixin.NodeTypeName, nodeType, default(INode))
        {
        }
    }
}
