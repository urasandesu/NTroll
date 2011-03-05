using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfLeft)
            {
                TypeDeclaration = Left == null ? null : Left.TypeDeclaration;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
