using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class Terminal<TValue> : Hierarchal<INode>, ITerminal<TValue>
    {
        public Terminal()
            : this(default(string), default(TValue), default(INode))
        {
            throw new NotSupportedException();
        }

        public Terminal(string name, TValue value, INode referrers)
            : base(name, referrers, Hierarchal<INode>.Empty)
        {
            Value = value;
        }

        public TValue Value { get; private set; }

        protected override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }
    }
}
