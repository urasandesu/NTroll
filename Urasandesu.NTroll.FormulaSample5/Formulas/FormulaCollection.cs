using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class FormulaCollection<TFormula> :
        Node, IList<TFormula>, ICollection<TFormula>, IEnumerable<TFormula>, 
        IList, ICollection, IEnumerable where TFormula : Formula
    {
        IList<TFormula> list;
        IList _list;

        public FormulaCollection()
        {
            Initialize(new Collection<TFormula>());
        }

        void Initialize(IList<TFormula> list)
        {
            if (!(list is IList)) 
                throw new ArgumentException("The parameter must implement System.Collections.IList.", TypeSavable.GetName(() => list));
            
            this.list = list;
            _list = (IList)list;
        }

        Formula referrer = new NullFormula();
        public Formula Referrer { get { return referrer; } set { referrer = CheckCanModify(value); } }

        #region IList<TFormula>, ICollection<TFormula>, IEnumerable<TFormula>, IList, ICollection, IEnumerable

        public int IndexOf(TFormula item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TFormula item)
        {
            item.Referrer = Referrer;
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list[index].Referrer = new NullFormula();
            list.RemoveAt(index);
        }

        public TFormula this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index].Referrer = Referrer;
                list[index] = value;
            }
        }

        public void Add(TFormula item)
        {
            item.Referrer = Referrer;
            list.Add(item);
        }

        public void Clear()
        {
            foreach (var item in list)
            {
                item.Referrer = new NullFormula();
            }
            list.Clear();
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
            item.Referrer = new NullFormula();
            return list.Remove(item);
        }

        public IEnumerator<TFormula> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int IList.Add(object value)
        {
            ((Formula)value).Referrer = Referrer;
            return _list.Add(value);
        }

        void IList.Clear()
        {
            foreach (Formula item in _list)
            {
                item.Referrer = new NullFormula();
            }
            _list.Clear();
        }

        bool IList.Contains(object value)
        {
            return _list.Contains(value);
        }

        int IList.IndexOf(object value)
        {
            return _list.IndexOf(value);
        }

        void IList.Insert(int index, object value)
        {
            ((Formula)value).Referrer = Referrer;
            _list.Insert(index, value);
        }

        bool IList.IsFixedSize
        {
            get { return _list.IsFixedSize; }
        }

        bool IList.IsReadOnly
        {
            get { return _list.IsReadOnly; }
        }

        void IList.Remove(object value)
        {
            ((Formula)value).Referrer = new NullFormula();
            _list.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            ((Formula)_list[index]).Referrer = new NullFormula();
            _list.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                ((Formula)_list[index]).Referrer = Referrer;
                _list[index] = value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return _list.Count; }
        }

        bool ICollection.IsSynchronized
        {
            get { return _list.IsSynchronized; }
        }

        object ICollection.SyncRoot
        {
            get { return _list.SyncRoot; }
        }

        #endregion

        protected override Node PinCore()
        {
            Initialize(new ReadOnlyCollection<TFormula>(list));
            return base.PinCore();
        }

        public override void AppendContentTo(StringBuilder sb)
        {
            this.AppendListContentTo(sb);
        }
    }
}
