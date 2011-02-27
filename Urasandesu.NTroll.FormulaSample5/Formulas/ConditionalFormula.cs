using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConditionalFormula : Formula
    {
        protected override void Initialize()
        {
            base.Initialize();
            PropertyChanged += new PropertyChangedEventHandler(ConditionalFormula_PropertyChanged);
        }

        void ConditionalFormula_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfIfTrue ||
                sender != this && e.PropertyName == ConditionalFormula.NameOfTypeDeclaration)
            {
                TypeDeclaration = IfTrue == null ? null : IfTrue.TypeDeclaration;
            }
        }
    }
}
