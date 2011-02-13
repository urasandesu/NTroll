using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class TerminalRef<TValue> : Hierarchal<INodeRef>, ITerminalRef<TValue>
    {
        public TerminalRef()
            : this(default(string), default(TValue), default(INodeRef))
        {
            throw new NotSupportedException();
        }

        public TerminalRef(string name, TValue value, INodeRef referrers)
            : base(name, referrers, Hierarchal<INodeRef>.Empty)
        {
            Value = value;
            Name = name;
            Referrers = referrers;
        }

        public TValue Value { get; set; }
        public new string Name { get; set; }
        public new INodeRef Referrers { get; set; }

        TValue ITerminal<TValue>.Value { get { return Value; } }
        string IHierarchal<INode>.Name { get { return Name; } }
        INode IHierarchal<INode>.Referrers { get { return Referrers; } }

        ITerminal<TValue> pinned;
        public ITerminal<TValue> Pin()
        {
            DoIfFirstHierarchy(() =>
            {
                var name = Name;
                var value = Value;
                var referrers = Referrers == null ? default(INode) : Referrers.Pin();
                pinned = Pin(name, value, referrers);
            });
            return pinned;
        }

        protected virtual ITerminal<TValue> Pin(string name, TValue value, INode referrers)
        {
            return new Terminal<TValue>(name, value, referrers);
        }

        INode INodeRef.Pin()
        {
            return Pin();
        }

        protected override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }

        #region IList<INode>, ICollection<INode>, IEnumerable<INode>, IEnumerable

        int IList<INode>.IndexOf(INode item)
        {
            return Hierarchal<INode>.Empty.IndexOf(item);
        }

        void IList<INode>.Insert(int index, INode item)
        {
            Hierarchal<INode>.Empty.Insert(index, item);
        }

        INode IList<INode>.this[int index]
        {
            get
            {
                return Hierarchal<INode>.Empty[index];
            }
            set
            {
                Hierarchal<INode>.Empty[index] = value;
            }
        }

        void ICollection<INode>.Add(INode item)
        {
            Hierarchal<INode>.Empty.Add(item);
        }

        bool ICollection<INode>.Contains(INode item)
        {
            return Hierarchal<INode>.Empty.Contains(item);
        }

        void ICollection<INode>.CopyTo(INode[] array, int arrayIndex)
        {
            Hierarchal<INode>.Empty.CopyTo(array, arrayIndex);
        }

        bool ICollection<INode>.Remove(INode item)
        {
            return Hierarchal<INode>.Empty.Remove(item);
        }

        IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
        {
            return Hierarchal<INode>.Empty.GetEnumerator();
        }

        #endregion
    }
}
