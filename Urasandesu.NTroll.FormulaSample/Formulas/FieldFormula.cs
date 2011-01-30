using System;
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
    public class FieldFormula : MemberFormula
    {
        protected internal FieldFormula(Formula instance, IFieldDeclaration field)
            : base(NodeType.Field, field.FieldType, instance, field)
        {
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
