using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Reflection;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using Urasandesu.NAnonym.Linq;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class NewFormula : Formula
    {
        public NewFormula(ConstructorInfo ci, Formula[] arguments)
        {
            Constructor = ci.ToConstructorDecl();
            arguments.AddRangeTo(Arguments);
        }

        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfConstructor)
            {
                TypeDeclaration = Constructor == null ? null : Constructor.DeclaringType;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
