using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface IHierarchal<THierarchal> : IList<THierarchal> where THierarchal : IHierarchal<THierarchal>
    {
        string Name { get; }
        THierarchal Referrers { get; }
        void DoIfFirstHierarchy(Action ifTrue);
        void DoIfFirstHierarchy(Action ifTrue, Action ifFalse);
    }
}
