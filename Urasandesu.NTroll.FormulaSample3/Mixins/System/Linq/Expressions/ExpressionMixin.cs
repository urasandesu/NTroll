using System.Linq.Expressions;
using Urasandesu.NTroll.FormulaSample3.Formulas;

namespace Urasandesu.NTroll.FormulaSample3.Mixins.System.Linq.Expressions
{
    public static class ExpressionMixin
    {
        public static object ToInlineValue(this Expression exp)
        {
            return ExpressionToInlineValue.Convert(exp);
        }
    }
}
