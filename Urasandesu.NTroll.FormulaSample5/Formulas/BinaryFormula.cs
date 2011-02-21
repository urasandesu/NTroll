using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        protected override void OnPropertyAppending(Node property, int propertiesIndex, StringBuilder sb)
        {
            if (propertiesIndex == MethodIndex)
            {
                AppendMethodTo(property, sb);
            }
            else
            {
                base.OnPropertyAppending(property, propertiesIndex, sb);
            }
        }

        protected abstract void AppendMethodTo(Node property, StringBuilder sb);
    }
}
