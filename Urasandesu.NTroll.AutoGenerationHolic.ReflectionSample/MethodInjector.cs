using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;
using System.Reflection;

namespace Urasandesu.NTroll.AutoGenerationHolic.ReflectionSample
{
    public class MethodInjector<T> where T : class, IDelegateEqualityComparerable<T>
    {
        DelegateEqualityComparer<T> comparer;
        public DelegateEqualityComparer<T> Comparer
        {
            get
            {
                if (comparer == null)
                {
                    var properties = typeof(T).GetPropertiesDefined<EqualityTargetWithComparerAttribute>().ToArray();
                    var equalsProvider = BuildMethodEquals(properties);
                    var getHashCodeProvider = BuildMethodGetHashCode(properties);
                    comparer = new DelegateEqualityComparer<T>(equalsProvider, getHashCodeProvider);
                }
                return comparer;
            }
        }

        public T Setup(T o)
        {
            var c = (IDelegateEqualityComparerable<T>)o;
            c.Comparer = Comparer;
            return o;
        }

        Func<T, object, bool> BuildMethodEquals(PropertyInfo[] properties)
        {
            var method = (Func<T, object, bool>)(
            (arg0, arg1) =>
            {
                var equals = default(bool);
                var that = default(T);
                if ((that = arg1 as T) == null)
                {
                    equals = false;
                }
                else
                {
                    var _this = arg0;
                    if (properties.Length == 0)
                    {
                        equals = _this.Equals(that);
                    }
                    else if (1 <= properties.Length)
                    {
                        var x = properties[0].GetValue(_this, null);
                        var y = properties[0].GetValue(that, null);
                        equals = x.Equals(y);
                        for (int i = 1; i < properties.Length; i++)
                        {
                            x = properties[i].GetValue(_this, null);
                            y = properties[i].GetValue(that, null);
                            equals = equals && x.Equals(y);
                        }
                    }
                }

                return equals;
            });
            return method;
        }

        Func<T, int> BuildMethodGetHashCode(PropertyInfo[] properties)
        {
            var method = (Func<T, int>)(
            arg0 =>
            {
                var hashCode = default(int);
                var _this = arg0;
                if (properties.Length == 0)
                {
                    hashCode = _this.GetHashCode();
                }
                else if (1 <= properties.Length)
                {
                    var obj = properties[0].GetValue(_this, null);
                    if (properties[0].PropertyType.IsValueType)
                    {
                        hashCode = obj.GetHashCode();
                    }
                    else
                    {
                        hashCode = obj == null ? 0 : obj.GetHashCode();
                    }

                    for (int i = 1; i < properties.Length; i++)
                    {
                        obj = properties[i].GetValue(_this, null);
                        if (properties[i].PropertyType.IsValueType)
                        {
                            hashCode = hashCode ^ obj.GetHashCode();
                        }
                        else
                        {
                            hashCode = hashCode ^ (obj == null ? 0 : obj.GetHashCode());
                        }
                    }
                }

                return hashCode;
            });
            return method;
        }
    }
}
