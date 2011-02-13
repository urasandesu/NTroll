using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
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

        void EvalExpression(Expression exp, State state)
        {
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
                    EvalMethodCall((MethodCallExpression)exp, state);
                    return;
                case ExpressionType.Coalesce:
                    throw new NotImplementedException();
                case ExpressionType.Conditional:
                    throw new NotImplementedException();
                case ExpressionType.Constant:
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
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

        void EvalMethodCall(MethodCallExpression exp, State state)
        {
            if (exp.Object == null)
            {
                if (exp.Method.DeclaringType.IsDefined(typeof(MethodReservedWordsAttribute), false))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
                }
            }
        }

        void EvalAllocAs(MethodCallExpression exp, State state)
        {
            //EvalExpression(exp.Object, state);
            //var left = state.CurrentBlock.Formulas.Pop<INodeRef>();
            //EvalExpression(exp.Arguments[0], state);
            //var right = state.CurrentBlock.Formulas.Pop<INodeRef>();
            //state.CurrentBlock.Formulas.Push(NonterminalRef.Assign(left, right));
        }

        public INonterminal GetCurrent()
        {
            return state.CurrentBlock.Pin();
        }

        class State
        {
            public State()
            {
                PushBlock();
                Arguments = new Collection<INonterminalRef>();
                Conditions = new Collection<IConditionalFormulaRef>();
            }

            public void PushBlock()
            {
                CurrentBlock = NonterminalRef.Block(CurrentBlock);
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
            public Collection<INonterminalRef> Arguments { get; private set; }
            public Collection<IConditionalFormulaRef> Conditions { get; private set; }
        }
    }

    public interface IConditionalFormula : INonterminal
    {
        INonterminal Test { get; }
        INonterminal IfTrue { get; }
        INonterminal IfFalse { get; }
    }

    public interface IConditionalFormulaRef : IConditionalFormula, INonterminalRef
    {
        new INonterminalRef Test { get; set; }
        new INonterminalRef IfTrue { get; set; }
        new INonterminalRef IfFalse { get; set; }
        new IConditionalFormula Pin();
    }
}
