using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    class ExpressionToInlineValue
    {
        protected ExpressionToInlineValue()
        {
        }

        public static object Convert(Expression exp)
        {
            return ConvertExpression(exp);
        }

        static object ConvertExpression(Expression exp)
        {
            if (exp == null) return null;

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
                    return ConvertMethodCall((MethodCallExpression)exp);
                case ExpressionType.Coalesce:
                    throw new NotImplementedException();
                case ExpressionType.Conditional:
                    throw new NotImplementedException();
                case ExpressionType.Constant:
                    return ConvertConstant((ConstantExpression)exp);
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
                    return ConvertMember((MemberExpression)exp);
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

        static object ConvertMethodCall(MethodCallExpression exp)
        {
            var parameter = new object[] { };
            if (exp.Arguments != null && 0 < exp.Arguments.Count)
            {
                parameter = (object[])ConvertArguments(exp.Arguments).ToArray();
            }

            if (exp.Object == null)
            {
                return exp.Method.Invoke(null, parameter);
            }
            else
            {
                return exp.Method.Invoke(ConvertExpression(exp.Object), parameter);
            }
        }

        static ReadOnlyCollection<object> ConvertArguments(ReadOnlyCollection<Expression> exps)
        {
            var arguments = new List<object>();
            foreach (var exp in exps)
            {
                arguments.Add(ConvertExpression(exp));
            }
            return new ReadOnlyCollection<object>(arguments);
        }

        static object ConvertConstant(ConstantExpression exp)
        {
            return exp.Value;
        }

        static object ConvertMember(MemberExpression exp)
        {
            var fieldInfo = default(FieldInfo);
            var propertyInfo = default(PropertyInfo);
            if ((fieldInfo = exp.Member as FieldInfo) != null)
            {
                if (exp.Expression == null)
                {
                    return fieldInfo.GetValue(null);
                }
                else
                {
                    return fieldInfo.GetValue(ConvertExpression(exp.Expression));
                }
            }
            else if ((propertyInfo = exp.Member as PropertyInfo) != null)
            {
                if (exp.Expression == null)
                {
                    return propertyInfo.GetValue(null, null);
                }
                else
                {
                    return propertyInfo.GetValue(ConvertExpression(exp.Expression), null);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
