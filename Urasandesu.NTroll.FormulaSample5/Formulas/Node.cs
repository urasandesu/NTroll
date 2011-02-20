using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract class Node
    {
        public Node()
            : this(default(string))
        {
        }

        public Node(string name)
        {
            Name = name;
        }

        public bool IsPinned { get; private set; }

        string name;
        public string Name { get { return name; } set { name = CheckCanModify(value); } }

        protected T CheckCanModify<T>(T value)
        {
            if (IsPinned)
            {
                throw new NotSupportedException("This object has already pinned, so it can not modify.");
            }
            return value;
        }

        public static Node Pin(Node item)
        {
            var pinned = item.PinCore();
            pinned.IsPinned = true;
            return pinned;
        }

        protected virtual Node PinCore()
        {
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }

        public void AppendTo(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append("\"");
                sb.Append(Name);
                sb.Append("\": ");
            }
            AppendContentTo(sb);
        }

        public abstract void AppendContentTo(StringBuilder sb);
    }
}
