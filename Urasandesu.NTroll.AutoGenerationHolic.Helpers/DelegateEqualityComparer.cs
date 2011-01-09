using System;

namespace Urasandesu.NTroll.AutoGenerationHolic.Helpers
{
    public class DelegateEqualityComparer<T>
    {
        Func<T, object, bool> equalsProvider;
        Func<T, int> getHashCodeProvider;
        public DelegateEqualityComparer(Func<T, object, bool> equalsProvider, Func<T, int> getHashCodeProvider)
        {
            this.equalsProvider = equalsProvider;
            this.getHashCodeProvider = getHashCodeProvider;
        }

        public bool Equals(T x, object y)
        {
            return equalsProvider(x, y);
        }

        public int GetHashCode(T obj)
        {
            return getHashCodeProvider(obj);
        }
    }
}
