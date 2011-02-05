using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    class ExpressionToFormula
    {
        State state;
        public ExpressionToFormula()
        {
            state = new State();
        }

        public void Eval(Expression exp)
        {
            EvalExpression(exp, state);
        }

        public IFormula GetCurrent()
        {
            return state.CurrentBlock.Establish();
        }

        void EvalExpression(Expression exp, State state)
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
                    throw new NotImplementedException();
                case ExpressionType.NewArrayBounds:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayInit:
                    throw new NotImplementedException();
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
                default:
                    throw new NotImplementedException();
            }
        }

        void EvalConditional(ConditionalExpression exp, State state)
        {
            EvalExpression(exp.Test, state);
            var test = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.IfTrue, state);
            var ifTrue = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.IfFalse, state);
            var ifFalse = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(FormulaRef.Condition(test, ifTrue, ifFalse));
        }

        void EvalUnary(UnaryExpression exp, State state)
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

        void EvalTypeAs(UnaryExpression exp, State state)
        {
            EvalExpression(exp.Operand, state);
            var operand = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(FormulaRef.TypeAs(operand, exp.Type));
        }

        void EvalConvert(UnaryExpression exp, State state)
        {
            EvalExpression(exp.Operand, state);
            var operand = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(FormulaRef.Convert(operand, exp.Type));
        }

        void EvalMember(MemberExpression exp, State state)
        {
            var fi = default(FieldInfo);
            var pi = default(PropertyInfo);
            if ((fi = exp.Member as FieldInfo) != null)
            {
                state.CurrentBlock.Formulas.Push(FormulaRef.Variable(fi.FieldType, fi.Name));
            }
            else if ((pi = exp.Member as PropertyInfo) != null)
            {
                state.CurrentBlock.Formulas.Push(FormulaRef.Variable(pi.PropertyType, pi.Name));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        void EvalBinary(BinaryExpression exp, State state)
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

        void EvalExclusiveOr(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var exclusiveOr = FormulaRef.ExclusiveOr(left, right);
            state.CurrentBlock.Formulas.Push(exclusiveOr);
        }

        void EvalMultiply(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var multiply = FormulaRef.Multiply(left, right);
            state.CurrentBlock.Formulas.Push(multiply);
        }

        void EvalAdd(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var add = FormulaRef.Add(left, right);
            state.CurrentBlock.Formulas.Push(add);
        }

        void EvalNotEqual(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var notEqual = FormulaRef.NotEqual(left, right);
            state.CurrentBlock.Formulas.Push(notEqual);
        }

        void EvalAndAlso(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var andAlso = FormulaRef.AndAlso(left, right);
            state.CurrentBlock.Formulas.Push(andAlso);
        }

        void EvalEqual(BinaryExpression exp, State state)
        {
            EvalExpression(exp.Left, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Right, state);
            var right = state.CurrentBlock.Formulas.Pop();
            var equal = FormulaRef.Equal(left, right);
            state.CurrentBlock.Formulas.Push(equal);
        }

        void EvalConstant(ConstantExpression exp, State state)
        {
            state.CurrentBlock.Formulas.Push(FormulaRef.Constant(exp.Value, exp.Type));
        }

        void EvalMethodCall(MethodCallExpression exp, State state)
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
                    else
                    {
                        throw new NotImplementedException();
                    }
                    //else if (exp.Method.IsDefined(typeof(MethodReservedWordIfAttribute), false)) EvalIf(exp, state);
                    //else if (exp.Method.IsDefined(typeof(MethodReservedWordElseIfAttribute), false)) EvalElseIf(exp, state);
                    //else if (exp.Method.IsDefined(typeof(MethodReservedWordEndIfAttribute), false)) EvalEndIf(exp, state);
                    //else
                    //{
                    //    throw new NotImplementedException();
                    //}
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
                        EvalInstanceMethodCall(exp, state);
                    }
                }
            }
        }

        void EvalReturn(MethodCallExpression exp, State state)
        {
            EvalExpression(exp.Arguments[0], state);
            var formula = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(FormulaRef.Return(formula));
        }

        void EvalStaticMethodCall(MethodCallExpression exp, State state)
        {
            EvalArguments(exp.Arguments, state);
            var arguments = new IFormulaRef[state.Arguments.Count];
            state.Arguments.MoveTo(arguments);
            state.CurrentBlock.Formulas.Push(FormulaRef.Call(exp.Method, arguments));
        }

        void EvalInstanceMethodCall(MethodCallExpression exp, State state)
        {
            EvalExpression(exp.Object, state);
            var instance = state.CurrentBlock.Formulas.Pop();
            EvalArguments(exp.Arguments, state);
            var arguments = new IFormulaRef[state.Arguments.Count];
            state.Arguments.MoveTo(arguments);
            state.CurrentBlock.Formulas.Push(FormulaRef.Call(instance, exp.Method, arguments));
        }

        void EvalArguments(ReadOnlyCollection<Expression> exps, State state)
        {
            var arguments = new List<IFormulaRef>();
            foreach (var exp in exps)
            {
                EvalExpression(exp, state);
                arguments.Add(state.CurrentBlock.Formulas.Pop());
            }
            arguments.AddRangeTo(state.Arguments);
        }

        void EvalMethodInfoInvoke_object_objects(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalConstructorInfoInvoke_objects(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalPropertyInfoSetValue_object_object_objects(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalPropertyInfoGetValue_object_objects(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalFieldInfoSetValue_object_object(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalFieldInfoGetValue_object(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalIf(MethodCallExpression exp, State state)
        {
            EvalExpression(exp.Arguments[0], state);
            var test = state.CurrentBlock.Formulas.Pop();
            var condition = FormulaRef.Condition(test);
            state.CurrentBlock.Formulas.Push(condition);
            state.Conditions.Push(condition);
            state.PushBlock();
            condition.IfTrue = state.CurrentBlock;
        }

        void EvalElseIf(MethodCallExpression exp, State state)
        {
            state.PopBlock();
            var prevCondition = state.Conditions.Pop();
            EvalExpression(exp.Arguments[0], state);
            var test = state.CurrentBlock.Formulas.Pop();
            var condition = FormulaRef.Condition(test);
            prevCondition.IfFalse = condition;
            state.Conditions.Push(condition);
            state.PushBlock();
            condition.IfTrue = state.CurrentBlock;
        }

        void EvalElse(MethodCallExpression exp, State state)
        {
            state.PopBlock();
            var prevCondition = state.Conditions.Pop();
            state.Conditions.Push(prevCondition);
            state.PushBlock();
            prevCondition.IfFalse = state.CurrentBlock;
        }

        void EvalEndIf(MethodCallExpression exp, State state)
        {
            state.Conditions.Pop();
            state.PopBlock();
        }

        void EvalAllocate(MethodCallExpression exp, State state)
        {
            if (exp.Arguments[0].NodeType == ExpressionType.MemberAccess)
            {
                var fi = (FieldInfo)((MemberExpression)exp.Arguments[0]).Member;
                var variable = FormulaRef.Variable(fi.FieldType, fi.Name);
                state.CurrentBlock.Formulas.Push(variable);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        void EvalAllocAs(MethodCallExpression exp, State state)
        {
            EvalExpression(exp.Object, state);
            var left = state.CurrentBlock.Formulas.Pop();
            EvalExpression(exp.Arguments[0], state);
            var right = state.CurrentBlock.Formulas.Pop();
            state.CurrentBlock.Formulas.Push(FormulaRef.Assign(left, right));
        }

        class State
        {
            public State()
            {
                PushBlock();
                Arguments = new Collection<IFormulaRef>();
                Conditions = new Collection<IConditionalFormulaRef>();
            }

            public void PushBlock()
            {
                CurrentBlock = FormulaRef.Block(CurrentBlock);
            }

            public void PopBlock()
            {
                if (CurrentBlock == null)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    CurrentBlock = CurrentBlock.ParentBlock;
                }
            }

            public IBlockFormulaRef CurrentBlock { get; private set; }
            public Collection<IFormulaRef> Arguments { get; private set; }
            public Collection<IConditionalFormulaRef> Conditions { get; private set; } 
        }
    }
}
