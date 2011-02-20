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
            : this(default(string))
        {
        }

        public FormulaCollection(string name)
            : this(new Collection<TFormula>())
        {
            Name = name;
        }

        public FormulaCollection(IList<TFormula> list)
        {
            if (!(list is IList)) 
                throw new ArgumentException("The parameter must implement System.Collections.IList.", TypeSavable.GetName(() => list));
            
            this.list = list;
            _list = (IList)list;
        }

        public int IndexOf(TFormula item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, TFormula item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
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
                list[index] = value;
            }
        }

        public void Add(TFormula item)
        {
            list.Add(item);
        }

        public void Clear()
        {
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
            return _list.Add(value);
        }

        void IList.Clear()
        {
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
            _list.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
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

        public override void AppendContentTo(StringBuilder sb)
        {
            this.AppendListContentTo(sb);
        }
    }
}
