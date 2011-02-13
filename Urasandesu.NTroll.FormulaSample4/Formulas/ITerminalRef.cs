using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface ITerminalRef<TValue> : ITerminal<TValue>, INodeRef
    {
        new TValue Value { get; set; }
        new ITerminal<TValue> Pin();
    }
}
