using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class Nonterminal : Hierarchal<INode>, INonterminal
    {
        public Nonterminal()
            : this(default(string), default(INode), new ReadOnlyCollection<INode>(new INode[] { }))
        {
            throw new NotSupportedException();
        }

        public Nonterminal(string name, INode referrers, IList<INode> properties)
            : base(name, referrers, properties)
        {
            NodeType = properties.GetNodeType();
            Type = properties.GetTypeDeclaration();
        }

        public ITerminal<NonterminalType> NodeType { get; private set; }
        public ITerminal<ITypeDeclaration> Type { get; private set; }

        public virtual INonterminal Accept(INonterminalVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }
    }
}
