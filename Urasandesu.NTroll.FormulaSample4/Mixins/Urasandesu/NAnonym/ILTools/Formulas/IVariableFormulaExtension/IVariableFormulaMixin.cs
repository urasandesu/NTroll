using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample4.Formulas;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IVariableFormulaExtension
{
    public static class IVariableFormulaMixin
    {
        public static readonly int VariableNameIndex = INonterminalMixin.TypeIndex + 1;

        public static Collection<INodeRef> ToProperties(ITerminalRef<ITypeDeclaration> type, ITerminalRef<string> variableName)
        {
            var list = (IList<INodeRef>)new List<INodeRef>();
            INonterminalMixin.InsertToProperties(new NodeTypeRef(NonterminalType.Variable), type, ref list);
            list.Insert(VariableNameIndex, variableName);
            return new Collection<INodeRef>(list);
        }

        public static ReadOnlyCollection<INode> ToReadOnlyProperties(ITerminal<ITypeDeclaration> type, ITerminal<string> variableName)
        {
            var list = (IList<INode>)new List<INode>();
            INonterminalMixin.InsertToProperties(new NodeType(NonterminalType.Variable), type, ref list);
            list.Insert(VariableNameIndex, variableName);
            return new ReadOnlyCollection<INode>(list);
        }

        public static ITerminal<string> GetVariableName(this IList<INode> source)
        {
            return (ITerminal<string>)source[VariableNameIndex];
        }

        public static ITerminalRef<string> SetVariableName(this IVariableFormula source, ITerminalRef<string> value)
        {
            source[VariableNameIndex] = value;
            return value;
        }

    }
}
