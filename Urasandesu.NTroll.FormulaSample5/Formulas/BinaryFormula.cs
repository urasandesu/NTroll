using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        public BinaryFormula(string name, FormulaType nodeType)
            : this(name, default(bool))
        {
            NodeType.Value = nodeType;
        }

        protected override void OnPropertyAppending(Node property, int propertyIndex, StringBuilder sb)
        {
            if (propertyIndex == MethodIndex)
            {
                AppendMethodTo(property, sb);
            }
            else
            {
                base.OnPropertyAppending(property, propertyIndex, sb);
            }
        }

        protected abstract void AppendMethodTo(Node property, StringBuilder sb);
    }
}
