using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample.Formulas
{
    public class NewArrayInitFormula : NewArrayFormula
    {
        public NewArrayInitFormula(ReadOnlyCollection<Formula> formulas)
            : base(NodeType.NewArrayInit, formulas)
        {
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
