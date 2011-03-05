using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class VariableFormula : Formula
    {
        public VariableFormula(string variableName, Type type)
        {
            VariableName = variableName;
            TypeDeclaration = type.ToTypeDecl();
        }
    }
}
