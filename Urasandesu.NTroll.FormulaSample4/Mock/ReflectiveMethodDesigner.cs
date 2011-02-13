using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample4.Formulas;

namespace Urasandesu.NTroll.FormulaSample4.Mock
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
        }

        public string Dump()
        {
            return etf.GetCurrent().ToString();
        }
    }
}
