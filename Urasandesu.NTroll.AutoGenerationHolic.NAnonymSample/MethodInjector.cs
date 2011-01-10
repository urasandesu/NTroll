using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;
using System.Reflection;
using System.Reflection.Emit;
using Urasandesu.NAnonym.Mixins.System.Reflection.Emit;

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
                var that = default(T);
                gen.Eval(_ => _.If(_.Alloc(that).As(_.LdArg(1) as T) == null));
                {
                    gen.Eval(_ => _.Return(false));
                }
                gen.Eval(_ => _.EndIf());

                if (properties.Length == 0)
                {
                    gen.Eval(_ => _.Return(_.LdArg(0).Equals(that)));
                }
                else if (properties.Length == 1)
                {
                    var gm = properties[0].GetGetMethod();
                    gen.Eval(_ => _.Return(_.AreEqual(_.Invoke(_.LdArg(0), _.X(gm)), _.Invoke(that, _.X(gm)))));
                }
                else if (2 <= properties.Length)
                {
                    var gm = properties[0].GetGetMethod();
                    var equals = default(bool);
                    gen.Eval(_ => _.Alloc(equals).As(_.AreEqual(_.Invoke(_.LdArg(0), _.X(gm)), _.Invoke(that, _.X(gm)))));
                    for (int i = 1; i < properties.Length; i++)
                    {
                        gm = properties[i].GetGetMethod();
                        gen.Eval(_ => _.Alloc(equals).As(equals && _.AreEqual(_.Invoke(_.LdArg(0), _.X(gm)), _.Invoke(that, _.X(gm)))));
                    }
                    gen.Eval(_ => _.Return(equals));
                }
            });
            return (Func<T, object, bool>)method.CreateDelegate(typeof(Func<T, object, bool>));
        }

        Func<T, int> BuildMethodGetHashCode(PropertyInfo[] properties)
        {
            var method = new DynamicMethod(string.Empty, typeof(int), new Type[] { typeof(T) });
            method.ExpressBody(
            gen =>
            {
                if (properties.Length == 0)
                {
                    gen.Eval(_ => _.Return(_.LdArg(0).GetHashCode()));
                }
                else if (properties.Length == 1)
                {
                    var gm = properties[0].GetGetMethod();
                    if (properties[0].PropertyType.IsValueType)
                    {
                        gen.Eval(_ => _.Return(_.Invoke(_.LdArg(0), _.X(gm)).GetHashCode()));
                    }
                    else
                    {
                        gen.Eval(_ => _.Return(_.Invoke(_.LdArg(0), _.X(gm)) == null ? 0 : _.Invoke(_.LdArg(0), _.X(gm)).GetHashCode()));
                    }
                }
                else if (2 <= properties.Length)
                {
                    var hashCode = default(int);
                    var gm = properties[0].GetGetMethod();
                    if (properties[0].PropertyType.IsValueType)
                    {
                        gen.Eval(_ => _.Alloc(hashCode).As(_.Invoke(_.LdArg(0), _.X(gm)).GetHashCode()));
                    }
                    else
                    {
                        gen.Eval(_ => _.Alloc(hashCode).As(_.Invoke(_.LdArg(0), _.X(gm)) == null ? 
                                                            0 : _.Invoke(_.LdArg(0), _.X(gm)).GetHashCode()));
                    }

                    for (int i = 1; i < properties.Length; i++)
                    {
                        gm = properties[i].GetGetMethod();
                        if (properties[i].PropertyType.IsValueType)
                        {
                            gen.Eval(_ => _.Alloc(hashCode).As(hashCode ^ _.Invoke(_.LdArg(0), _.X(gm)).GetHashCode()));
                        }
                        else
                        {
                            gen.Eval(_ => _.Alloc(hashCode).As(hashCode ^ (_.Invoke(_.LdArg(0), _.X(gm)) == null ? 
                                                                            0 : _.Invoke(_.LdArg(0), _.X(gm)).GetHashCode())));
                        }
                    }

                    gen.Eval(_ => _.Return(hashCode));
                }
            });
            return (Func<T, int>)method.CreateDelegate(typeof(Func<T, int>));
        }
    }
}
