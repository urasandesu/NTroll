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
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class Formula : INotifyPropertyChanged
    {
        protected virtual void Initialize()
        {
        }

        public bool IsPinned { get; private set; }

        protected void CheckCanModify(Formula formula)
        {
            if (formula != null && formula.IsPinned)
            {
                throw new NotSupportedException("This object has already pinned, so it can not modify.");
            }
        }

        protected void SetValue<T>(string propertyName, T value, ref T result)
        {
            CheckCanModify(this);

            result = value;

            if (Referrer != null)
            {
                Referrer.OnPropertyChanged(propertyName);
            }
            OnPropertyChanged(propertyName);
        }

        protected void SetValueWithoutNotification<T>(string propertyName, T value, ref T result)
        {
            CheckCanModify(this);

            result = value;
        }

        protected void SetFormula<TFormula>(string propertyName, TFormula formula, ref TFormula result) where TFormula : Formula
        {
            SetReferrerWithoutNotification(result, null);
            Unsubscribe(result);
            SetValue(propertyName, formula, ref result);
            Subscribe(result);
            SetReferrerWithoutNotification(result, this);
        }

        protected void SetFormulaWithoutNotification<TFormula>(string propertyName, TFormula formula, ref TFormula result) where TFormula : Formula
        {
            SetValueWithoutNotification(propertyName, formula, ref result);
        }

        protected void SetReferrerWithoutNotification(Formula target, Formula referrer)
        {
            if (target != null)
            {
                CheckCanModify(target.Referrer);
                target.referrer = referrer;
            }
        }

        protected virtual void Subscribe(Formula target)
        {
            if (target != null)
            {
                target.PropertyChanged += OnPropertyChanged;
            }
        }

        protected virtual void Unsubscribe(Formula target)
        {
            if (target != null)
            {
                target.PropertyChanged -= OnPropertyChanged;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(sender, e);
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
