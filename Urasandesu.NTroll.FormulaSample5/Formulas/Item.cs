using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class Item<T> : Node
    {
        public Item()
            : this(default(string))
        {
        }

        public Item(string name)
            : this(name, default(T))
        {
        }

        public Item(string name, T value)
            : base()
        {
            this.Name = name;
            this.value = value;
        }

        T value = default(T);
        public T Value { get { return this.value; } set { this.value = CheckCanModify(value); } }

        public override void AppendContentTo(StringBuilder sb)
        {
            AppendItemContentTo(sb);
        }

        public void AppendItemContentTo(StringBuilder sb)
        {
            if (!(Value is ValueType) && Value.IsDefault())
            {
                sb.Append("null");
            }
            else
            {
                var s = Value.ToString();
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
