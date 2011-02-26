using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;
using System.Collections.ObjectModel;
using System.Collections;
using System.Runtime.Serialization;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class Formula
    {
        public bool IsPinned { get; private set; }

        protected T CheckCanModify<T>(T value)
        {
            if (IsPinned)
            {
                throw new NotSupportedException("This object has already pinned, so it can not modify.");
            }
            return value;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
        }

        public static Formula Pin(Formula item)
        {
            var pinned = default(Formula);
            if (item != null && !item.IsPinned)
            {
                pinned = item.PinCore();
                pinned.IsPinned = true;
            }
            return pinned;
        }

        public static void AppendListTo<TFormula>(IList<TFormula> formulas, StringBuilder sb)
            where TFormula : Formula
        {
            sb.Append("[");
            var oneOrMore = false;
            foreach (var formula in formulas)
            {
                if (!oneOrMore)
                {
                    oneOrMore = true;
                    formula.AppendTo(sb);
                }
                else
                {
                    sb.Append(", ");
                    formula.AppendTo(sb);
                }
            }
            sb.Append("]");
        }

        public static void AppendValueTo<TValue>(TValue value, StringBuilder sb)
        {
            AppendValueTo(value, sb, null);
        }

        public static void AppendValueTo<TValue>(TValue value, StringBuilder sb, string ifDefault)
        {
            if (!(value is ValueType) && value.IsDefault())
            {
                sb.Append(ifDefault == null ? "null" : ifDefault);
            }
            else
            {
                var s = value.ToString();
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            AppendTo(sb);
            return sb.ToString();
        }

        public abstract Formula Accept(IFormulaVisitor visitor);
    }
}
