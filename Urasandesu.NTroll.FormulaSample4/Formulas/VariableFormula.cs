using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IVariableFormulaExtension;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class VariableFormula : Nonterminal, IVariableFormula
    {
        protected internal VariableFormula(string name, INode referrers, ITerminal<ITypeDeclaration> type, ITerminal<string> variableName)
            : this(name, referrers, IVariableFormulaMixin.ToReadOnlyProperties(type, variableName))
        {
        }

        protected internal VariableFormula(string name, INode referrers, ReadOnlyCollection<INode> properties)
            : base(name, referrers, properties)
        {
            VariableName = properties.GetVariableName();
        }

        public ITerminal<string> VariableName { get; private set; }

        public override INonterminal Accept(INonterminalVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
