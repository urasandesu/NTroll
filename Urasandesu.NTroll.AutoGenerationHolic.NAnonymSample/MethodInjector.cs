using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Urasandesu.NAnonym.Mixins.System.Reflection.Emit;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;
using Urasandesu.NAnonym.Mixins.Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.AutoGenerationHolic.NAnonymSample
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
            var method = new DynamicMethod(string.Empty, typeof(bool), new Type[] { typeof(T), typeof(object) });
            method.ExpressBody(
            gen =>
            {
                var equals = default(bool);
                var that = default(T);
                gen.Eval(_ => _.If(_.Alloc(that).As(_.LdArg<T>(1) as T) == null));
                {
                    gen.Eval(_ => _.Alloc(equals).As(false));
                }
                gen.Eval(_ => _.Else());
                {
                    var _this = default(T);
                    gen.Eval(_ => _.Alloc(_this).As(_.LdArg<T>(0)));
                    if (properties.Length == 0)
                    {
                        gen.Eval(_ => _.Alloc(equals).As(_this.Equals(that)));
                    }
                    else if (1 <= properties.Length)
                    {
                        var x = default(object);
                        gen.Eval(_ => _.Alloc(x).As(properties[0].GetValue(_this, null)));
                        var y = default(object);
                        gen.Eval(_ => _.Alloc(y).As(properties[0].GetValue(that, null)));
                        gen.Eval(_ => _.Alloc(equals).As(x.Equals(y)));
                        for (int i = 1; i < properties.Length; i++)
                        {
                            gen.Eval(_ => _.Alloc(x).As(properties[i].GetValue(_this, null)));
                            gen.Eval(_ => _.Alloc(y).As(properties[i].GetValue(that, null)));
                            gen.Eval(_ => _.Alloc(equals).As(equals && x.Equals(y)));
                        }
                    }
                }
                gen.Eval(_ => _.EndIf());

                gen.Eval(_ => _.Return(equals));
            });
            return (Func<T, object, bool>)method.CreateDelegate(typeof(Func<T, object, bool>));
        }

        Func<T, int> BuildMethodGetHashCode(PropertyInfo[] properties)
        {
            var method = new DynamicMethod(string.Empty, typeof(int), new Type[] { typeof(T) });
            method.ExpressBody(
            gen =>
            {
                var hashCode = default(int);
                var _this = default(T);
                gen.Eval(_ => _.Alloc(_this).As(_.LdArg<T>(0)));
                if (properties.Length == 0)
                {
                    gen.Eval(_ => _.Alloc(hashCode).As(_this.GetHashCode()));
                }
                else if (1 < properties.Length)
                {
                    var obj = default(object);
                    gen.Eval(_ => _.Alloc(obj).As(properties[0].GetValue(_this, null)));
                    if (properties[0].PropertyType.IsValueType)
                    {
                        gen.Eval(_ => _.Alloc(hashCode).As(obj.GetHashCode()));
                    }
                    else
                    {
                        gen.Eval(_ => _.Alloc(hashCode).As(obj == null ? 0 : obj.GetHashCode()));
                    }

                    for (int i = 1; i < properties.Length; i++)
                    {
                        gen.Eval(_ => _.Alloc(obj).As(properties[i].GetValue(_this, null)));
                        if (properties[i].PropertyType.IsValueType)
                        {
                            gen.Eval(_ => _.Alloc(hashCode).As(hashCode ^ obj.GetHashCode()));
                        }
                        else
                        {
                            gen.Eval(_ => _.Alloc(hashCode).As(hashCode ^ (obj == null ? 0 : obj.GetHashCode())));
                        }
                    }
                }

                gen.Eval(_ => _.Return(hashCode));
            });
            return (Func<T, int>)method.CreateDelegate(typeof(Func<T, int>));
        }
    }
}
