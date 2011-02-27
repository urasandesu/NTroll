using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        protected override void Initialize()
        {
            base.Initialize();
            PropertyChanged += new PropertyChangedEventHandler(BinaryFormula_PropertyChanged);
        }

        void BinaryFormula_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfLeft)
            {
                TypeDeclaration = Left == null ? null : Left.TypeDeclaration;
            }
        }
    }
}
