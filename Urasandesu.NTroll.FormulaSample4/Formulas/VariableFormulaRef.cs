using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IVariableFormulaExtension;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class VariableFormulaRef : NonterminalRef, IVariableFormulaRef
    {
        protected internal VariableFormulaRef(string name, INodeRef referrers, ITerminalRef<ITypeDeclaration> type, ITerminalRef<string> variableName)
            : base(name, referrers, IVariableFormulaMixin.ToProperties(type, variableName))
        {
            this.variableName = variableName;
        }

        ITerminalRef<string> variableName;
        public ITerminalRef<string> VariableName { get { return variableName; } set { variableName = this.SetVariableName(value); } }
        ITerminal<string> IVariableFormula.VariableName { get { return VariableName; } }

        public new IVariableFormula Pin()
        {
            return (IVariableFormula)base.Pin();
        }

        protected override INonterminal PinCore(string name, INode referrers, ReadOnlyCollection<INode> properties)
        {
            return new VariableFormula(name, referrers, properties);
        }
    }
}
