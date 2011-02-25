using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class ExpressionToInlineValueState
    {
        public ExpressionToInlineValueState()
        {
            Arguments = new Collection<object>();
        }

        public Collection<object> Arguments { get; private set; }
        public object Result { get; set; }
    }
}
