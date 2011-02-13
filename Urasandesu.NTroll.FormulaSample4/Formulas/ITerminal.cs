using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface ITerminal<TValue> : INode
    {
        TValue Value { get; }
    }

    public static class ITerminalMixin
    {
        public static void DumpToString<TValue>(this ITerminal<TValue> source, StringBuilder sb)
        {
            if (source.Value.IsDefault())
            {
                if (source.Value is ValueType)
                {
                    sb.Append(source.Value.ToString());
                }
                else
                {
                    sb.Append("null");
                }
            }
            else
            {
                var s = source.Value.ToString();
                var result = default(double);
                if (double.TryParse(s, out result))
                {
                    sb.Append(s);
                }
                else
                {
                    sb.Append("\"");
                    sb.Append(s);
                    sb.Append("\"");
                }
            }
        }
    }
}
