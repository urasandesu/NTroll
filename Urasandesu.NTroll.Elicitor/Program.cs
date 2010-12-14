using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Urasandesu.NTroll.Elicitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "aiueo";
            Console.WriteLine(new Elicitor().Elicit(() => a));

            var b = new B();
            b.Name = "kakikukeko";
            Console.WriteLine(new Elicitor().Elicit(() => b.Name));

            var c = new C();
            c.Type = typeof(int);
            Console.WriteLine(new Elicitor().Elicit(() => c.Type.AssemblyQualifiedName));

            var d = new D(new string[] { "katayama", "watabe", "mochida" });
            Console.WriteLine(string.Join(", ", new Elicitor().Elicit(() => d.GetNames())));

            var e = new E((MethodInfo)MethodBase.GetCurrentMethod());
            Console.WriteLine(string.Join(", ", new Elicitor().Elicit(() => e.Method.GetParameterNames())));
        }

        class B
        {
            public string Name { get; set; }
        }

        class C
        {
            public Type Type { get; set; }
        }

        class D
        {
            readonly string[] names;
            public D(string[] names)
            {
                this.names = names;
            }

            public string[] GetNames()
            {
                return names;
            }
        }

        class E
        {
            public E(MethodInfo method)
            {
                Method = method;
            }

            public MethodInfo Method { get; private set; }
        }
    }

    static class Extension
    {
        public static string[] GetParameterNames(this MethodInfo source)
        {
            return source.GetParameters().Select(parameter => parameter.Name).ToArray();
        }
    }

    class Elicitor
    {
        public TResult Elicit<TResult>(Expression<Func<TResult>> exp)
        {
            var state = new ElicitState();
            ElicitExpression(exp.Body, state);
            return (TResult)state.Value;
        }

        static void ElicitExpression(Expression exp, ElicitState state)
        {
            if (exp == null) return;

            switch (exp.NodeType)
            {
                case ExpressionType.Add:
                    throw new NotImplementedException();
                case ExpressionType.AddChecked:
                    throw new NotImplementedException();
                case ExpressionType.And:
                    throw new NotImplementedException();
                case ExpressionType.AndAlso:
                    throw new NotImplementedException();
                case ExpressionType.ArrayIndex:
                    throw new NotImplementedException();
                case ExpressionType.ArrayLength:
                    throw new NotImplementedException();
                case ExpressionType.Call:
                    ElicitCall((MethodCallExpression)exp, state);
                    break;
                case ExpressionType.Coalesce:
                    throw new NotImplementedException();
                case ExpressionType.Conditional:
                    throw new NotImplementedException();
                case ExpressionType.Constant:
                    ElicitConstant((ConstantExpression)exp, state);
                    break;
                case ExpressionType.Convert:
                    throw new NotImplementedException();
                case ExpressionType.ConvertChecked:
                    throw new NotImplementedException();
                case ExpressionType.Divide:
                    throw new NotImplementedException();
                case ExpressionType.Equal:
                    throw new NotImplementedException();
                case ExpressionType.ExclusiveOr:
                    throw new NotImplementedException();
                case ExpressionType.GreaterThan:
                    throw new NotImplementedException();
                case ExpressionType.GreaterThanOrEqual:
                    throw new NotImplementedException();
                case ExpressionType.Invoke:
                    throw new NotImplementedException();
                case ExpressionType.Lambda:
                    throw new NotImplementedException();
                case ExpressionType.LeftShift:
                    throw new NotImplementedException();
                case ExpressionType.LessThan:
                    throw new NotImplementedException();
                case ExpressionType.LessThanOrEqual:
                    throw new NotImplementedException();
                case ExpressionType.ListInit:
                    throw new NotImplementedException();
                case ExpressionType.MemberAccess:
                    ElicitMemberAccess((MemberExpression)exp, state);
                    break;
                case ExpressionType.MemberInit:
                    throw new NotImplementedException();
                case ExpressionType.Modulo:
                    throw new NotImplementedException();
                case ExpressionType.Multiply:
                    throw new NotImplementedException();
                case ExpressionType.MultiplyChecked:
                    throw new NotImplementedException();
                case ExpressionType.Negate:
                    throw new NotImplementedException();
                case ExpressionType.NegateChecked:
                    throw new NotImplementedException();
                case ExpressionType.New:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayBounds:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayInit:
                    throw new NotImplementedException();
                case ExpressionType.Not:
                    throw new NotImplementedException();
                case ExpressionType.NotEqual:
                    throw new NotImplementedException();
                case ExpressionType.Or:
                    throw new NotImplementedException();
                case ExpressionType.OrElse:
                    throw new NotImplementedException();
                case ExpressionType.Parameter:
                    throw new NotImplementedException();
                case ExpressionType.Power:
                    throw new NotImplementedException();
                case ExpressionType.Quote:
                    throw new NotImplementedException();
                case ExpressionType.RightShift:
                    throw new NotImplementedException();
                case ExpressionType.Subtract:
                    throw new NotImplementedException();
                case ExpressionType.SubtractChecked:
                    throw new NotImplementedException();
                case ExpressionType.TypeAs:
                    throw new NotImplementedException();
                case ExpressionType.TypeIs:
                    throw new NotImplementedException();
                case ExpressionType.UnaryPlus:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        static void ElicitCall(MethodCallExpression exp, ElicitState state)
        {
            var parameter = new object[] { };
            if (exp.Arguments != null && 0 < exp.Arguments.Count)
            {
                ElicitArguments(exp.Arguments, state);
                parameter = (object[])state.Value;
            }

            if (exp.Object == null)
            {
                state.Value = exp.Method.Invoke(null, parameter);
            }
            else
            {
                ElicitExpression(exp.Object, state);
                state.Value = exp.Method.Invoke(state.Value, parameter);
            }
        }

        static void ElicitArguments(ReadOnlyCollection<Expression> exps, ElicitState state)
        {
            var arguments = new List<object>();
            foreach (var exp in exps)
            {
                ElicitExpression(exp, state);
                arguments.Add(state.Value);
            }
            state.Value = arguments.ToArray();
        }

        static void ElicitConstant(ConstantExpression exp, ElicitState state)
        {
            state.Value = exp.Value;
        }

        static void ElicitMemberAccess(MemberExpression exp, ElicitState state)
        {
            var fieldInfo = default(FieldInfo);
            var propertyInfo = default(PropertyInfo);
            if ((fieldInfo = exp.Member as FieldInfo) != null)
            {
                if (exp.Expression == null)
                {
                    state.Value = fieldInfo.GetValue(null);
                }
                else
                {
                    ElicitExpression(exp.Expression, state);
                    state.Value = fieldInfo.GetValue(state.Value);
                }
            }
            else if ((propertyInfo = exp.Member as PropertyInfo) != null)
            {
                if (exp.Expression == null)
                {
                    state.Value = propertyInfo.GetValue(null, null);
                }
                else
                {
                    ElicitExpression(exp.Expression, state);
                    state.Value = propertyInfo.GetValue(state.Value, null);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        internal class ElicitState
        {
            public object Value { get; set; }
        }
    }
}
