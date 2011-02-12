using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample3.Formulas;

namespace Urasandesu.NTroll.FormulaSample3.Mock
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
            throw new NotImplementedException();
        }
    }
}
