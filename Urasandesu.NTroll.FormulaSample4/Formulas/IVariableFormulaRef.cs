using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface IVariableFormulaRef : IVariableFormula, INonterminalRef
    {
        new ITerminalRef<string> VariableName { get; set; }
        new IVariableFormula Pin();
    }
}
