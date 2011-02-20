using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample5.Formulas;

namespace Urasandesu.NTroll.FormulaSample5.Mock
{
    public class ReflectiveMethodDesigner   // Eval(() => Dsl.End()); で確定させるイメージ。
    {
        ExpressionToFormulaState state;
        public ReflectiveMethodDesigner()
        {
            state = new ExpressionToFormulaState();
        }

        public void Eval(Expression<Action> exp)
        {
            ExpressionToFormula.Eval(exp.Body, state);
        }

        public string Dump()
        {
            return state.CurrentBlock.ToString();
        }
    }
}
