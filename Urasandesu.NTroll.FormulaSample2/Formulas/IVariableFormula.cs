
using Urasandesu.NAnonym.ILTools;
using System.Text;
using System;
using Urasandesu.NAnonym;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IVariableFormula : IFormula
    {
        string Name { get; }
    }
}
