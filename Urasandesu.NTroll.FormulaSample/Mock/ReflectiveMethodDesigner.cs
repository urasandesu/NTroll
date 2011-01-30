using System;
using System.Linq.Expressions;
using Urasandesu.NTroll.FormulaSample.Formulas;
using Urasandesu.NTroll.FormulaSample.Mixins.System.Linq.Expressions;

namespace Urasandesu.NTroll.FormulaSample.Mock
{
    public class ReflectiveMethodDesigner
    {
        ExpressionToFormula etf;
        public ReflectiveMethodDesigner()
        {
            etf = new ExpressionToFormula();
        }

        public void Eval(Expression<Action> exp)
        {
            etf.Eval(exp.Body);
            //var formula = exp.Body.ToFormula();         // まずは一通り変形。
            ////formula.Accept(new FormulaTypeResolver());  // 型チェックおよび Conversion の最適化。
            //Console.WriteLine(formula);
        }

        public string Dump()
        {
            return etf.GetResult().ToString();
        }
    }

    //public class FormulaTypeResolver : IFormulaVisitor
    //{
    //}
}
