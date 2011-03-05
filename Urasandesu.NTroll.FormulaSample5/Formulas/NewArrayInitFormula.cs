using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Linq;
using System.ComponentModel;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewArrayInitFormula : Formula
    {
        public NewArrayInitFormula(Formula[] formulas, Type type)
        {
            formulas.AddRangeTo(Formulas);
            TypeDeclaration = type.ToTypeDecl();
        }
    }
}
