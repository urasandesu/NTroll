using System.Linq.Expressions;
using Urasandesu.NTroll.FormulaSample4.Formulas;

namespace Urasandesu.NTroll.FormulaSample4.Mixins.System.Linq.Expressions
{
    public static class ExpressionMixin
    {
        public static object ToInlineValue(this Expression exp)
        {
            return ExpressionToInlineValue.Convert(exp);
        }
    }
}
