using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using System.Collections;
using Urasandesu.NAnonym;
using Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IBlockFormulaExtension;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class NonterminalRef : Hierarchal<INodeRef>, INonterminalRef
    {
        public NonterminalRef()
            : this(default(string), default(INodeRef), new Collection<INodeRef>())
        {
            throw new NotSupportedException();
        }

        public NonterminalRef(string name, INodeRef referrers)
            : this(name, referrers, new Collection<INodeRef>())
        {
        }

        public NonterminalRef(string name, INodeRef referrers, IList<INodeRef> properties)
            : base(name, referrers, properties)
        {
            this.nodeType = properties.GetNodeType();
            this.type = properties.GetTypeDeclaration();
        }

        public new string Name { get { return base.Name; } set { base.Name = value; } }
        public new INodeRef Referrers { get { return base.Referrers; } set { base.Referrers = value; } }
        ITerminalRef<NonterminalType> nodeType;
        public ITerminalRef<NonterminalType> NodeType { get { return nodeType; } set { nodeType = this.SetNodeType(value); } }
        ITerminalRef<ITypeDeclaration> type;
        public ITerminalRef<ITypeDeclaration> Type { get { return type; } set { type = this.SetType(value); } }

        string IHierarchal<INode>.Name { get { return Name; } }
        INode IHierarchal<INode>.Referrers { get { return Referrers; } }
        ITerminal<NonterminalType> INonterminal.NodeType { get { return NodeType; } }
        ITerminal<ITypeDeclaration> INonterminal.Type { get { return Type; } }

        public INonterminal Accept(INonterminalVisitor visitor)
        {
            throw new InvalidOperationException("Must convert this to an object that implements INonterminal by calling Pin().");
        }

        INonterminal pinned;
        public INonterminal Pin()
        {
            DoIfFirstHierarchy(() =>
            {
                var name = Name;
                var referrers = Referrers == null ? default(INode) : Referrers.Pin();
                var properties = new List<INode>();
                foreach (var property in this)
                {
                    properties.Add(property == null ? default(INode) : property.Pin());
                }
                pinned = PinCore(name, referrers, new ReadOnlyCollection<INode>(properties));
            });
            return pinned;
        }

        INode INodeRef.Pin()
        {
            return Pin();
        }

        protected virtual INonterminal PinCore(string name, INode referrers, ReadOnlyCollection<INode> properties)
        {
            throw new NotImplementedException();
        }

        protected override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }

        #region IList<INode>, ICollection<INode>, IEnumerable<INode>, IEnumerable

        int IList<INode>.IndexOf(INode item)
        {
            return IndexOf((INodeRef)item);
        }

        void IList<INode>.Insert(int index, INode item)
        {
            Insert(index, (INodeRef)item);
        }

        INode IList<INode>.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (INodeRef)value;
            }
        }

        void ICollection<INode>.Add(INode item)
        {
            Add((INodeRef)item);
        }

        bool ICollection<INode>.Contains(INode item)
        {
            return Contains((INodeRef)item);
        }

        void ICollection<INode>.CopyTo(INode[] array, int arrayIndex)
        {
            CopyTo((INodeRef[])array, arrayIndex);
        }

        bool ICollection<INode>.Remove(INode item)
        {
            return Remove((INodeRef)item);
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
        {
            return this.Cast<INode>().GetEnumerator();
        }

        #endregion

        public static IBlockFormulaRef Block(IBlockFormulaRef parentBlock)
        {
            return new BlockFormulaRef(
                default(string), 
                default(INodeRef), 
                default(ITerminalRef<ITypeDeclaration>), 
                parentBlock, 
                default(IBlockFormulaRef), 
                default(INonterminalRef), 
                new NonterminalRef(IBlockFormulaMixin.ParentBlockName, null), 
                null);
        }

        internal static INode Assign(INonterminalRef left, INonterminalRef right)
        {
            throw new NotImplementedException();
        }
    }

}
