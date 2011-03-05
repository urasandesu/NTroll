using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ReturnFormula : Formula
    {
        public ReturnFormula(Formula body)
        {
            Body = body;
        }

        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfBody)
            {
                TypeDeclaration = Body == null ? null : Body.TypeDeclaration;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
