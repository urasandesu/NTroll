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
            : this()
        {
            Body = body;
        }

        protected override void Initialize()
        {
            base.Initialize();
            PropertyChanged += new PropertyChangedEventHandler(ReturnFormula_PropertyChanged);
        }

        void ReturnFormula_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfBody)
            {
                TypeDeclaration = Body == null ? null : Body.TypeDeclaration;
            }
        }
    }
}
