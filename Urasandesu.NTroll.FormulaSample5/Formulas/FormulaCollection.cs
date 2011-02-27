using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using Urasandesu.NAnonym;
using System.Collections.Specialized;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class FormulaCollection<TFormula> :
        Formula, IList<TFormula>, ICollection<TFormula>, IEnumerable<TFormula>, INotifyCollectionChanged where TFormula : Formula
    {
        IList<TFormula> list;

        public FormulaCollection()
        {
            Initialize(new Collection<TFormula>());
        }

        public FormulaCollection(params TFormula[] formulas)
        {
            Initialize(new Collection<TFormula>(formulas));
        }

        void Initialize(IList<TFormula> list)
        {
            this.list = list;
        }

        public int IndexOf(TFormula item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TFormula item)
        {
            list.Insert(index, item);
            SetReferrerWithoutNotification(item, this);
            Subscribe(item);
            OnCountPropertyChanged();
            OnItemPropertyChanged();
            OnCollectionAdded(item, index);
        }

        public void RemoveAt(int index)
        {
            var removingItem = list[index];
            list.RemoveAt(index);
            SetReferrerWithoutNotification(removingItem, null);
            Unsubscribe(removingItem);
            OnCountPropertyChanged();
            OnItemPropertyChanged();
            OnCollectionRemoved(removingItem, index);
        }

        public TFormula this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                var replacingItem = list[index];
                list[index] = value;
                SetReferrerWithoutNotification(replacingItem, null);
                Unsubscribe(replacingItem);
                SetReferrerWithoutNotification(value, Referrer);
                Subscribe(value);
                OnItemPropertyChanged();
                OnCollectionReplaced(value, replacingItem, index);
            }
        }

        public void Add(TFormula item)
        {
            Insert(Count, item);
        }

        public void Clear()
        {
            foreach (var removingItem in list)
            {
                SetReferrerWithoutNotification(removingItem, null);
                Unsubscribe(removingItem);
            }
            list.Clear();
            OnCountPropertyChanged();
            OnItemPropertyChanged();
            OnCollectionReset();
        }

        public bool Contains(TFormula item)
        {
            return list.Contains(item);
        }

        public void CopyTo(TFormula[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public bool Remove(TFormula item)
        {
            var success = list.Remove(item);
            if (success)
            {
                SetReferrerWithoutNotification(item, null);
                Unsubscribe(item);
                OnCountPropertyChanged();
                OnItemPropertyChanged();
                OnCollectionRemoved(item);
            }
            return success;
        }

        public IEnumerator<TFormula> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public const string NameOfCount = "Count";
        protected void OnCountPropertyChanged()
        {
            OnPropertyChanged(NameOfCount);
        }

        public const string NameOfItem = "Item[]";
        protected void OnItemPropertyChanged()
        {
            OnPropertyChanged(NameOfItem);
        }

        protected void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected void OnCollectionAdded(TFormula item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        protected void OnCollectionRemoved(TFormula item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        protected void OnCollectionRemoved(TFormula item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }

        protected void OnCollectionReplaced(TFormula newItem, TFormula oldItem, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index));
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            OnCollectionChanged(this, e);
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, e);
            }
        }

        protected override Formula PinCore()
        {
            Initialize(new ReadOnlyCollection<TFormula>(list));
            return base.PinCore();
        }

        public override void AppendTo(StringBuilder sb)
        {
            AppendListTo(this, sb);
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
