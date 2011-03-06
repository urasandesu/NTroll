﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public static class ExpressionToFormula
    {
        public static void EvalTo(this Expression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp, state);
        }

        public static void EvalExpression(Expression exp, ExpressionToFormulaState state)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.AddChecked:
                    throw new NotImplementedException();
                case ExpressionType.And:
                    throw new NotImplementedException();
                case ExpressionType.ArrayIndex:
                    throw new NotImplementedException();
                case ExpressionType.ArrayLength:
                    throw new NotImplementedException();
                case ExpressionType.Call:
                    EvalMethodCall((MethodCallExpression)exp, state);
                    return;
                case ExpressionType.Coalesce:
                    throw new NotImplementedException();
                case ExpressionType.Conditional:
                    EvalConditional((ConditionalExpression)exp, state);
                    return;
                case ExpressionType.Constant:
                    EvalConstant((ConstantExpression)exp, state);
                    return;
                case ExpressionType.Convert:
                case ExpressionType.TypeAs:
                    EvalUnary((UnaryExpression)exp, state);
                    return;
                case ExpressionType.ConvertChecked:
                    throw new NotImplementedException();
                case ExpressionType.Divide:
                    throw new NotImplementedException();
                case ExpressionType.Add:
                case ExpressionType.Multiply:
                case ExpressionType.AndAlso:
                case ExpressionType.ExclusiveOr:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    EvalBinary((BinaryExpression)exp, state);
                    return;
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
                    EvalMember((MemberExpression)exp, state);
                    return;
                case ExpressionType.MemberInit:
                    throw new NotImplementedException();
                case ExpressionType.Modulo:
                    throw new NotImplementedException();
                case ExpressionType.MultiplyChecked:
                    throw new NotImplementedException();
                case ExpressionType.Negate:
                    throw new NotImplementedException();
                case ExpressionType.NegateChecked:
                    throw new NotImplementedException();
                case ExpressionType.New:
                    EvalNew((NewExpression)exp, state);
                    return;
                case ExpressionType.NewArrayBounds:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayInit:
                    EvalNewArray((NewArrayExpression)exp, state);
                    return;
                case ExpressionType.Not:
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
                case ExpressionType.TypeIs:
                    throw new NotImplementedException();
                case ExpressionType.UnaryPlus:
                    throw new NotImplementedException();
            }
        }

        public static void EvalNew(NewExpression exp, ExpressionToFormulaState state)
        {
            EvalArguments(exp.Arguments, state);
            var arguments = new Formula[state.Arguments.Count];
            state.Arguments.MoveTo(arguments);
            state.CurrentBlock.Formulas.Push(new NewFormula(exp.Constructor, arguments));
        }

        public static void EvalNewArray(NewArrayExpression exp, ExpressionToFormulaState state)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.NewArrayInit:
                    EvalNewArrayInit(exp, state);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void EvalNewArrayInit(NewArrayExpression exp, ExpressionToFormulaState state)
        {
            EvalArguments(exp.Expressions, state);
            var arguments = new Formula[state.Arguments.Count];
            state.Arguments.MoveTo(arguments);
            state.CurrentBlock.Formulas.Push(new NewArrayInitFormula(arguments, exp.Type));
        }

        public static void EvalArguments(ReadOnlyCollection<Expression> exps, ExpressionToFormulaState state)
        {
            foreach (var exp in exps)
            {
                EvalExpression(exp, state);
                var formula = state.CurrentBlock.Formulas.Pop();
                state.Arguments.Add(formula);
            }
        }

        public static void EvalConditional(ConditionalExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Test, state);
            var test = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.IfTrue, state);
            var ifTrue = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.IfFalse, state);
            var ifFalse = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new ConditionalFormula() { Test = test, IfTrue = ifTrue, IfFalse = ifFalse });
        }

        public static void EvalBinary(BinaryExpression exp, ExpressionToFormulaState state)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Add:
                    EvalAdd(exp, state);
                    return;
                case ExpressionType.Multiply:
                    EvalMultiply(exp, state);
                    return;
                case ExpressionType.AndAlso:
                    EvalAndAlso(exp, state);
                    return;
                case ExpressionType.ExclusiveOr:
                    EvalExclusiveOr(exp, state);
                    return;
                case ExpressionType.Equal:
                    EvalEqual(exp, state);
                    return;
                case ExpressionType.NotEqual:
                    EvalNotEqual(exp, state);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void EvalExclusiveOr(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var exclusiveOr = new ExclusiveOrFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(exclusiveOr);
        }

        public static void EvalEqual(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var equal = new EqualFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(equal);
        }

        public static void EvalAndAlso(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var andAlso = new AndAlsoFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(andAlso);
        }

        public static void EvalMultiply(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var multiply = new MultiplyFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(multiply);
        }

        public static void EvalAdd(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var add = new AddFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(add);
        }

        public static void EvalNotEqual(BinaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var notEqual = new NotEqualFormula() { Left = left, Right = right };
            state.CurrentBlock.Formulas.Push(notEqual);
        }

        public static void EvalMember(MemberExpression exp, ExpressionToFormulaState state)
        {
            var fi = default(FieldInfo);
            var pi = default(PropertyInfo);
            if ((fi = exp.Member as FieldInfo) != null)
            {
                state.CurrentBlock.Formulas.Push(new VariableFormula(fi.Name, fi.FieldType));
            }
            else if ((pi = exp.Member as PropertyInfo) != null)
            {
                state.CurrentBlock.Formulas.Push(new VariableFormula(pi.Name, pi.PropertyType));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public static void EvalUnary(UnaryExpression exp, ExpressionToFormulaState state)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Convert:
                    EvalConvert(exp, state);
                    return;
                case ExpressionType.TypeAs:
                    EvalTypeAs(exp, state);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void EvalTypeAs(UnaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Operand, state);
            var operand = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new TypeAsFormula(operand, exp.Type));
        }

        public static void EvalConvert(UnaryExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Operand, state);
            var operand = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new ConvertFormula(operand, exp.Type));
        }

        public static void EvalConstant(ConstantExpression exp, ExpressionToFormulaState state)
        {
            state.CurrentBlock.Formulas.Push(new ConstantFormula(exp.Value, exp.Type));
        }

        public static void EvalMethodCall(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            if (exp.Object == null)
            {
                if (exp.Method.DeclaringType.IsDefined(typeof(MethodReservedWordsAttribute), false))
                {
                    if (exp.Method.IsDefined(typeof(MethodReservedWordAllocateAttribute), false)) EvalAllocate(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordIfAttribute), false)) EvalIf(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordElseIfAttribute), false)) EvalElseIf(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordElseAttribute), false)) EvalElse(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordEndIfAttribute), false)) EvalEndIf(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordReturnAttribute), false)) EvalReturn(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordConstMemberAttribute), false)) EvalConstMember(exp, state);
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordEndAttribute), false)) EvalEnd(exp, state);
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    EvalStaticMethodCall(exp, state);
                }
            }
            else
            {
                if (exp.Object.Type.IsDefined(typeof(MethodAllocReservedWordsAttribute), false))
                {
                    if (exp.Method.IsDefined(typeof(MethodAllocReservedWordAsAttribute), false)) EvalAllocAs(exp, state);
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    if (exp.Method == MethodInfoMixin.Invoke_object_objects) EvalMethodInfoInvoke_object_objects(exp, state);
                    else if (exp.Method == ConstructorInfoMixin.Invoke_objects) EvalConstructorInfoInvoke_objects(exp, state);
                    else if (exp.Method == PropertyInfoMixin.SetValue_object_object_objects) EvalPropertyInfoSetValue_object_object_objects(exp, state);
                    else if (exp.Method == PropertyInfoMixin.GetValue_object_objects) EvalPropertyInfoGetValue_object_objects(exp, state);
                    else if (exp.Method == FieldInfoMixin.SetValue_object_object) EvalFieldInfoSetValue_object_object(exp, state);
                    else if (exp.Method == FieldInfoMixin.GetValue_object) EvalFieldInfoGetValue_object(exp, state);
                    else
                    {
                        throw new NotImplementedException();
                        //EvalInstanceMethodCall(exp, state);
                    }
                }
            }
        }

        public static void EvalEnd(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            var end = new EndFormula() { TypeDeclaration = state.CurrentBlock.TypeDeclaration };
            state.CurrentBlock.Formulas.Push(end);
            state.IsEnded = true;
        }

        public static void EvalConstMember(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Arguments[0].ConvertTo(state.InlineValueState);
            var constMember = state.InlineValueState.Result;
            exp.Arguments[1].ConvertTo(state.InlineValueState);
            var type = (Type)state.InlineValueState.Result;
            if (!state.ConstMembersCache.ContainsKey(type))
            {
                state.ConstMembersCache.Add(type, new Dictionary<object, FieldInfo>());
                var fis = type.GetFields(BindingFlags.Public | BindingFlags.Static);
                foreach (var fi in fis.Where(_ => _.IsInitOnly))
                {
                    state.ConstMembersCache[type].Add(fi.GetValue(null), fi);
                }
            }
            if (!state.ConstMembersCache[type].ContainsKey(constMember))
            {
                throw new NotSupportedException("Dsl.ConstMember can only use to a field that is static and init only.");
            }
            else
            {
                var fi = state.ConstMembersCache[type][constMember];
                state.CurrentBlock.Formulas.Push(new FieldFormula(null, fi));
            }
        }

        public static void EvalStaticMethodCall(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            EvalArguments(exp.Arguments, state);
            var arguments = new Formula[state.Arguments.Count];
            state.Arguments.MoveTo(arguments);
            state.CurrentBlock.Formulas.Push(new CallFormula(null, exp.Method, arguments));
        }

        public static void EvalMethodInfoInvoke_object_objects(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var mi = (MethodInfo)state.InlineValueState.Result;
            var instance = default(Formula);
            if (!mi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentBlock.Formulas.Pop();
            }
            var arguments = new Formula[] { };
            if (exp.Arguments[1].NodeType == ExpressionType.NewArrayInit)
            {
                EvalArguments(((NewArrayExpression)exp.Arguments[1]).Expressions, state);
                arguments = new Formula[state.Arguments.Count];
                state.Arguments.MoveTo(arguments);
            }
            else
            {
                throw new NotImplementedException();
            }
            state.CurrentBlock.Formulas.Push(new ReflectiveCallFormula(instance, mi, arguments));
        }

        public static void EvalConstructorInfoInvoke_objects(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var ci = (ConstructorInfo)state.InlineValueState.Result;
            var arguments = new Formula[] { };
            if (exp.Arguments[0].NodeType == ExpressionType.NewArrayInit)
            {
                EvalArguments(((NewArrayExpression)exp.Arguments[0]).Expressions, state);
                arguments = new Formula[state.Arguments.Count];
                state.Arguments.MoveTo(arguments);
            }
            else if (exp.Arguments[0].NodeType == ExpressionType.Constant && ((ConstantExpression)exp.Arguments[0]).Value == null)
            {
                // discard...
            }
            else
            {
                throw new NotImplementedException();
            }
            state.CurrentBlock.Formulas.Push(new ReflectiveNewFormula(ci, arguments));
        }

        public static void EvalPropertyInfoSetValue_object_object_objects(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var pi = (PropertyInfo)state.InlineValueState.Result;
            var setter = pi.GetSetMethod(true);
            var instance = default(Formula);
            if (!setter.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentBlock.Formulas.Pop();
            }
            var left = new ReflectivePropertyFormula(instance, pi);
            EvalExpression(exp.Arguments[1], state);
            var right = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new AssignFormula() { Left = left, Right = right });
        }

        public static void EvalPropertyInfoGetValue_object_objects(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var pi = (PropertyInfo)state.InlineValueState.Result;
            var getter = pi.GetGetMethod(true);
            var instance = default(Formula);
            if (!getter.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentBlock.Formulas.Pop();
            }
            state.CurrentBlock.Formulas.Push(new ReflectivePropertyFormula(instance, pi));
        }

        public static void EvalFieldInfoSetValue_object_object(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var fi = (FieldInfo)state.InlineValueState.Result;
            var instance = default(Formula);
            if (!fi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentBlock.Formulas.Pop();
            }
            var left = new ReflectiveFieldFormula(instance, fi);
            EvalExpression(exp.Arguments[1], state);
            var right = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new AssignFormula() { Left = left, Right = right });
        }

        public static void EvalFieldInfoGetValue_object(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            exp.Object.ConvertTo(state.InlineValueState);
            var fi = (FieldInfo)state.InlineValueState.Result;
            var instance = default(Formula);
            if (!fi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentBlock.Formulas.Pop();
            }
            state.CurrentBlock.Formulas.Push(new ReflectiveFieldFormula(instance, fi));
        }

        public static void EvalIf(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Arguments[0], state);
            var test = state.CurrentBlock.Formulas.Pop();
            var condition = new ConditionalFormula() { Test = test };
            state.CurrentBlock.Formulas.Push(condition);
            state.Conditions.Push(condition);
            state.PushBlock();
            condition.IfTrue = state.CurrentBlock;
        }

        public static void EvalElseIf(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            state.PopBlock();
            var prevCondition = state.Conditions.Pop();
            EvalExpression(exp.Arguments[0], state);
            var test = state.CurrentBlock.Formulas.Pop();
            var condition = new ConditionalFormula() { Test = test };
            prevCondition.IfFalse = condition;
            state.Conditions.Push(condition);
            state.PushBlock();
            condition.IfTrue = state.CurrentBlock;
        }

        public static void EvalElse(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            state.PopBlock();
            var prevCondition = state.Conditions.Pop();
            state.Conditions.Push(prevCondition);
            state.PushBlock();
            prevCondition.IfFalse = state.CurrentBlock;
        }

        public static void EvalEndIf(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            state.Conditions.Pop();
            state.PopBlock();
        }

        public static void EvalReturn(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Arguments[0], state);
            var body = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new ReturnFormula(body));
        }

        public static void EvalAllocAs(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            EvalExpression(exp.Object, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Arguments[0], state);
            var right = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(new AssignFormula() { Left = left, Right = right });
        }

        public static void EvalAllocate(MethodCallExpression exp, ExpressionToFormulaState state)
        {
            if (exp.Arguments[0].NodeType == ExpressionType.MemberAccess)
            {
                var fi = (FieldInfo)((MemberExpression)exp.Arguments[0]).Member;
                var variable = new VariableFormula(fi.Name, fi.FieldType);
                state.CurrentBlock.Formulas.Push(variable);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

    }

}
