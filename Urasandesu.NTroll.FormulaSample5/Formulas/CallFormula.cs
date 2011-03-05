using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Urasandesu.NAnonym.Linq;
using Urasandesu.NAnonym.Mixins.System.Reflection;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class CallFormula : Formula
    {
        public CallFormula(Formula instance, MethodInfo mi, Formula[] arguments)
            : base()
        {
            Instance = instance;
            Method = mi.ToMethodDecl();
            arguments.AddRangeTo(Arguments);
        }

        protected override bool ReceivePropertyChangedCore(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NameOfMethod)
            {
                TypeDeclaration = Method == null ? null : Method.ReturnType;
            }
            return base.ReceivePropertyChangedCore(sender, e);
        }
    }
}
