using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Urasandesu.NAnonym;
using System.Collections.ObjectModel;
using System.Reflection;
using Urasandesu.NTroll.FormulaSample.Formulas;

namespace Urasandesu.NTroll.FormulaSample.Mixins.System.Linq.Expressions
{
    public static class ExpressionMixin
    {
        public static Formula ToFormula(this Expression exp)
        {
            var etf = new ExpressionToFormula();
            etf.Eval(exp);
            return etf.GetResult();
        }

        public static object ToInlineValue(this Expression exp)
        {
            return ExpressionToInlineValue.Convert(exp);
        }
    }
}
