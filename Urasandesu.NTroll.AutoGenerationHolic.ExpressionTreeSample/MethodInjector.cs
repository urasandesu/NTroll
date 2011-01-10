using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers.Mixins.System;

namespace Urasandesu.NTroll.AutoGenerationHolic.ExpressionTreeSample
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
            var x = Expression.Parameter(typeof(T), "x");
            var obj = Expression.Parameter(typeof(object), "obj");
            var typeIs = Expression.TypeIs(obj, typeof(T));
            var typeIsNot = Expression.Not(typeIs);
            var constFalse = Expression.Constant(false, typeof(bool));
            var y = Expression.Convert(obj, typeof(T));

            var body = default(Expression);
            if (properties.Length == 0)
            {
                var baseEquals = typeof(T).BaseType.GetMethod("Equals");
                body = Expression.Condition(typeIsNot, constFalse, Expression.Call(x, baseEquals, obj));
            }
            else if (properties.Length == 1)
            {
                var equal = Expression.Equal(Expression.Property(x, properties[0]), Expression.Property(y, properties[0]));
                body = Expression.Condition(typeIsNot, constFalse, equal);
            }
            else if (2 <= properties.Length)
            {
                var equal = Expression.Equal(Expression.Property(x, properties[0]), Expression.Property(y, properties[0]));
                var andAlso = Expression.AndAlso(equal, Expression.Equal(Expression.Property(x, properties[1]), Expression.Property(y, properties[1])));
                for (int i = 2; i < properties.Length; i++)
                {
                    andAlso = Expression.AndAlso(andAlso, Expression.Equal(Expression.Property(x, properties[i]), Expression.Property(y, properties[i])));
                }
                body = Expression.Condition(typeIsNot, constFalse, andAlso);
            }

            return Expression.Lambda<Func<T, object, bool>>(body, x, obj).Compile();
        }

        Func<T, int> BuildMethodGetHashCode(PropertyInfo[] properties)
        {
            var x = Expression.Parameter(typeof(T), "x");
            var constNull = Expression.Constant(null, typeof(object));
            var constZero = Expression.Constant(0, typeof(int));
            var body = default(Expression);
            if (properties.Length == 0)
            {
                var baseGetHashCode = typeof(T).BaseType.GetMethod("GetHashCode");
                body = Expression.Call(x, baseGetHashCode);
            }
            else if (properties.Length == 1)
            {
                var getHashCode = properties[0].PropertyType.GetMethod("GetHashCode");
                var callGetHashCode = Expression.Call(Expression.Property(x, properties[0]), getHashCode);
                if (properties[0].PropertyType.IsValueType)
                {
                    body = callGetHashCode;
                }
                else
                {
                    var isNull = Expression.Equal(Expression.Property(x, properties[0]), constNull);
                    body = Expression.Condition(isNull, constZero, callGetHashCode);
                }
            }
            else if (2 <= properties.Length)
            {
                var _body = default(Expression);
                var getHashCode = default(MethodInfo);
                var callGetHashCode = default(MethodCallExpression);
                getHashCode = properties[0].PropertyType.GetMethod("GetHashCode");
                callGetHashCode = Expression.Call(Expression.Property(x, properties[0]), getHashCode);
                var _getHashCode = properties[1].PropertyType.GetMethod("GetHashCode");
                var _callGetHashCode = Expression.Call(Expression.Property(x, properties[1]), _getHashCode);
                var xor = default(BinaryExpression);
                if (properties[0].PropertyType.IsValueType)
                {
                    _body = callGetHashCode;
                }
                else
                {
                    var isNull = Expression.Equal(Expression.Property(x, properties[0]), constNull);
                    _body = Expression.Condition(isNull, constZero, callGetHashCode);
                }

                if (properties[1].PropertyType.IsValueType)
                {
                    xor = Expression.ExclusiveOr(_body, _callGetHashCode);
                }
                else
                {
                    var isNull = Expression.Equal(Expression.Property(x, properties[1]), constNull);
                    xor = Expression.ExclusiveOr(_body, Expression.Condition(isNull, constZero, _callGetHashCode));
                }

                for (int i = 2; i < properties.Length; i++)
                {
                    _getHashCode = properties[i].PropertyType.GetMethod("GetHashCode");
                    _callGetHashCode = Expression.Call(Expression.Property(x, properties[i]), _getHashCode);
                    if (properties[i].PropertyType.IsValueType)
                    {
                        xor = Expression.ExclusiveOr(xor, _callGetHashCode);
                    }
                    else
                    {
                        var isNull = Expression.Equal(Expression.Property(x, properties[i]), constNull);
                        xor = Expression.ExclusiveOr(xor, Expression.Condition(isNull, constZero, _callGetHashCode));
                    }
                }

                body = xor;
            }

            return Expression.Lambda<Func<T, int>>(body, x).Compile();
        }
    }
}
