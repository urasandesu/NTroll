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
    public class ConstantFormula : Formula
    {
        protected internal ConstantFormula(object value, ITypeDeclaration type)
            : base(NodeType.Constant, type)
        {
            Value = value;
        }

        public override Formula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public object Value { get; private set; }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"Value\": ");
            sb.Append("\""); 
            sb.Append(Value.NullableToString());
            sb.Append("\"");
        }
    }
}
