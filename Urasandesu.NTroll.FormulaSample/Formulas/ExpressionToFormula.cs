using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using System.Reflection;
using Urasandesu.NAnonym.Linq;
using Urasandesu.NTroll.FormulaSample.Mixins.System.Linq.Expressions;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    class ExpressionToFormula
    {
        State state;

        public ExpressionToFormula()
        {
            state = new State();
            state.PushScope(new Scope());
        }

        public void Eval(Expression exp)
        {
            Required.NotDefault(exp, () => exp);
            EvalExpression(exp, state);
        }

        public Formula GetResult()
        {
            return state.CurrentScope.PeekFormula();
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
                    EvalConstant((ConstantExpression)exp, state);
                    return;
                case ExpressionType.Convert:
                    EvalUnary((UnaryExpression)exp, state);
                    return;
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
                    EvalMember((MemberExpression)exp, state);
                    return;
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
                    EvalNew((NewExpression)exp, state);
                    return;
                case ExpressionType.NewArrayBounds:
                    throw new NotImplementedException();
                case ExpressionType.NewArrayInit:
                    EvalNewArray((NewArrayExpression)exp, state);
                    return;
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

        void EvalNewArray(NewArrayExpression exp, State state)
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

        void EvalNewArrayInit(NewArrayExpression exp, State state)
        {
            EvalArguments(exp.Expressions, state);
            var arguments = state.CurrentScope.PopFormulas();
            state.CurrentScope.PushFormula(Formula.NewArrayInit(arguments));
        }

        void EvalConstant(ConstantExpression exp, State state)
        {
            state.CurrentScope.PushFormula(Formula.Constant(exp.Value));
        }

        void EvalUnary(UnaryExpression exp, State state)
        {
            EvalExpression(exp.Operand, state);
            var operand = state.CurrentScope.PopFormula();
            EvalUnaryWithoutOperandEval(exp, operand, state);
        }

        void EvalUnaryWithoutOperandEval(UnaryExpression exp, Formula operand, State state)
        {
            switch (exp.NodeType)
            {
                case ExpressionType.Convert:
                    EvalConvertWithoutOperandEval(exp, operand, state);
                    return;
                default:
                    throw new NotImplementedException();
            }
        }

        void EvalConvertWithoutOperandEval(UnaryExpression exp, Formula operand, State state)
        {
            state.CurrentScope.PushFormula(Formula.Convert(operand, exp.Type));
        }

        void EvalMember(MemberExpression exp, State state)
        {
            var fi = default(FieldInfo);
            var pi = default(PropertyInfo);
            if ((fi = exp.Member as FieldInfo) != null)
            {
                state.CurrentScope.PushFormula(Formula.Variable(fi.FieldType, fi.Name));
            }
            else if ((pi = exp.Member as PropertyInfo) != null)
            {
                state.CurrentScope.PushFormula(Formula.Variable(pi.PropertyType, pi.Name));
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        void EvalArguments(ReadOnlyCollection<Expression> exps, State state)
        {
            var arguments = new Stack<Formula>();
            foreach (var exp in exps)
            {
                EvalExpression(exp, state);
                arguments.Push(state.CurrentScope.PopFormula());
            }
            state.CurrentScope.PushFormulas(arguments);
        }

        void EvalNew(NewExpression exp, State state)
        {
            EvalArguments(exp.Arguments, state);
            var arguments = state.CurrentScope.PopFormulas();
            state.CurrentScope.PushFormula(Formula.New(exp.Constructor, arguments));
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
                    else if (exp.Method.IsDefined(typeof(MethodReservedWordEndIfAttribute), false)) EvalEndIf(exp, state);
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                else
                {
                    EvalArguments(exp.Arguments, state);
                    var arguments = state.CurrentScope.PopFormulas();
                    state.CurrentScope.PushFormula(Formula.Call(null, exp.Method, arguments));
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
                    }
                }
            }
        }

        void EvalIf(MethodCallExpression exp, State state)
        {
            EvalExpression(exp, state);
            var test = state.CurrentScope.PopFormula();
            state.CurrentScope.PushFormula(test);
            state.PushScope(new Scope());
        }

        void EvalElseIf(MethodCallExpression exp, State state)
        {
            var scope = state.PopScope();
            //state.pop
            throw new NotImplementedException();
        }

        void EvalEndIf(MethodCallExpression exp, State state)
        {
            throw new NotImplementedException();
        }

        void EvalConstructorInfoInvoke_objects(MethodCallExpression exp, State state)
        {
            var ci = (ConstructorInfo)exp.Object.ToInlineValue();
            var arguments = default(ReadOnlyCollection<Formula>);
            if (exp.Arguments[0].NodeType == ExpressionType.NewArrayInit)
            {
                EvalArguments(((NewArrayExpression)exp.Arguments[0]).Expressions, state);
                arguments = state.CurrentScope.PopFormulas();
            }
            else if (exp.Arguments[0].NodeType == ExpressionType.Constant && ((ConstantExpression)exp.Arguments[0]).Value == null)
            {
                // discard...
            }
            else
            {
                throw new NotImplementedException();
            }
            state.CurrentScope.PushFormula(Formula.New(ci, arguments));
        }

        void EvalPropertyInfoGetValue_object_objects(MethodCallExpression exp, State state)
        {
            var pi = (PropertyInfo)exp.Object.ToInlineValue();
            var getter = pi.GetGetMethod(true);
            var instance = default(Formula);
            if (!getter.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentScope.PopFormula();
            }
            state.CurrentScope.PushFormula(Formula.Property(instance, pi));
        }

        void EvalPropertyInfoSetValue_object_object_objects(MethodCallExpression exp, State state)
        {
            var pi = (PropertyInfo)exp.Object.ToInlineValue();
            var setter = pi.GetSetMethod(true);
            var instance = default(Formula);
            if (!setter.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentScope.PopFormula();
            }
            var left = Formula.Property(instance, pi);
            EvalExpression(exp.Arguments[1], state);
            var right = state.CurrentScope.PopFormula();
            state.CurrentScope.PushFormula(Formula.Assign(left, right));
        }

        void EvalMethodInfoInvoke_object_objects(MethodCallExpression exp, State state)
        {
            var mi = (MethodInfo)exp.Object.ToInlineValue();
            var instance = default(Formula);
            if (!mi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentScope.PopFormula();
            }
            var arguments = default(ReadOnlyCollection<Formula>);
            if (exp.Arguments[1].NodeType == ExpressionType.NewArrayInit)
            {
                EvalArguments(((NewArrayExpression)exp.Arguments[1]).Expressions, state);
                arguments = state.CurrentScope.PopFormulas();
            }
            else
            {
                throw new NotImplementedException();
            }
            state.CurrentScope.PushFormula(Formula.Call(instance, mi, arguments));
        }

        void EvalFieldInfoGetValue_object(MethodCallExpression exp, State state)
        {
            var fi = (FieldInfo)exp.Object.ToInlineValue();
            var instance = default(Formula);
            if (!fi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentScope.PopFormula();
            }
            state.CurrentScope.PushFormula(Formula.Field(instance, fi));
        }

        void EvalFieldInfoSetValue_object_object(MethodCallExpression exp, State state)
        {
            var fi = (FieldInfo)exp.Object.ToInlineValue();
            var instance = default(Formula);
            if (!fi.IsStatic)
            {
                EvalExpression(exp.Arguments[0], state);
                instance = state.CurrentScope.PopFormula();
            }
            var left = Formula.Field(instance, fi);
            EvalExpression(exp.Arguments[1], state);
            var right = state.CurrentScope.PopFormula();
            state.CurrentScope.PushFormula(Formula.Assign(left, right));
        }

        void EvalAllocate(MethodCallExpression exp, State state)
        {
            if (exp.Arguments[0].NodeType == ExpressionType.MemberAccess)
            {
                var fi = (FieldInfo)((MemberExpression)exp.Arguments[0]).Member;
                var variable = Formula.Variable(fi.FieldType, fi.Name);
                state.CurrentScope.PushFormulaWithVariable(variable);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        void EvalAllocAs(MethodCallExpression exp, State state)
        {
            EvalExpression(exp.Object, state);
            var left = state.CurrentScope.PopFormula();
            EvalExpression(exp.Arguments[0], state);
            var right = state.CurrentScope.PopFormula();
            state.CurrentScope.PushFormula(Formula.Assign(left, right));
        }

        class State
        {
            public State()
            {
                scopes = new Stack<Scope>();
            }

            Stack<Scope> scopes;
            public void PushScope(Scope scope)
            {
                scopes.Push(scope);
            }
            public Scope PopScope()
            {
                return scopes.Pop();
            }
            public Scope CurrentScope { get { return scopes.Peek(); } }
        }

        class Scope
        {
            public Scope()
            {
                formulas = new Stack<Formula>();
                variables = new Stack<Formula>();
                Children = new Stack<Scope>();
            }

            public Scope Parent { get; set; }
            Stack<Formula> formulas;
            Stack<Formula> variables;
            public Stack<Scope> Children { get; private set; }

            public void PushFormula(Formula formula)
            {
                formulas.Push(formula);
            }

            public void PushFormulas(IEnumerable<Formula> formulas)
            {
                foreach (var formula in formulas)
                {
                    this.formulas.Push(formula);
                }
            }

            public Formula PeekFormula()
            {
                return this.formulas.Peek();
            }

            public Formula PopFormula()
            {
                return this.formulas.Pop();
            }

            public ReadOnlyCollection<Formula> PopFormulas()
            {
                var formulas = this.formulas.ToArray();
                this.formulas.Clear();
                return new ReadOnlyCollection<Formula>(formulas);
            }

            public void PushFormulaWithVariable(Formula variable)
            {
                PushFormula(variable);
                variables.Push(variable);
            }
        }
    }
}
