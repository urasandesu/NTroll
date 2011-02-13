using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public abstract class Hierarchal<THierarchal> : IHierarchal<THierarchal> where THierarchal : IHierarchal<THierarchal>
    {
        public static readonly EmptyHierarchal<THierarchal> Empty = new EmptyHierarchal<THierarchal>();

        protected Hierarchal()
        {
            throw new NotSupportedException();
        }

        IList<THierarchal> list;
        protected Hierarchal(string name, THierarchal referrers, IList<THierarchal> list)
        {
            Name = name;
            Referrers = referrers;
            this.list = list;
        }

        public string Name { get; protected set; }
        public THierarchal Referrers { get; protected set; }

        int hierarchy;
        public void DoIfFirstHierarchy(Action ifTrue)
        {
            DoIfFirstHierarchy(ifTrue, () => { });
        }

        public void DoIfFirstHierarchy(Action ifTrue, Action ifFalse)
        {
            try
            {
                if (hierarchy++ == 0)
                {
                    ifTrue();
                }
                else
                {
                    ifFalse();
                }
            }
            finally
            {
                hierarchy--;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Name))
            {
                sb.Append("\"");
                sb.Append(Name);
                sb.Append("\": ");
            }
            ContentToString(sb);
            return sb.ToString();
        }

        protected abstract void ContentToString(StringBuilder sb);

        #region IList<THierarchal>

        public int IndexOf(THierarchal item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, THierarchal item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public THierarchal this[int index]
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

        public void Add(THierarchal item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(THierarchal item)
        {
            return list.Contains(item);
        }

        public void CopyTo(THierarchal[] array, int arrayIndex)
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

        public bool Remove(THierarchal item)
        {
            return list.Remove(item);
        }

        public IEnumerator<THierarchal> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    public class EmptyHierarchal<THierarchal> : IHierarchal<THierarchal> where THierarchal : IHierarchal<THierarchal>
    {
        public string Name { get { return default(string); } }
        public THierarchal Referrers { get { return default(THierarchal); } }
        public void DoIfFirstHierarchy(Action ifTrue) { }
        public void DoIfFirstHierarchy(Action ifTrue, Action ifFalse) { }
        public int IndexOf(THierarchal item) { return default(int); }
        public void Insert(int index, THierarchal item) { }
        public void RemoveAt(int index) { }
        public THierarchal this[int index] { get { return default(THierarchal); } set { } }
        public void Add(THierarchal item) { }
        public void Clear() { }
        public bool Contains(THierarchal item) { return default(bool); }
        public void CopyTo(THierarchal[] array, int arrayIndex) { }
        public int Count { get { return default(int); } }
        public bool IsReadOnly { get { return default(bool); } }
        public bool Remove(THierarchal item) { return default(bool); }
        public IEnumerator<THierarchal> GetEnumerator() { return new List<THierarchal>().GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return new THierarchal[] { }.GetEnumerator(); }
    }
}
