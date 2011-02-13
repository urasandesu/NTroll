using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NTroll.FormulaSample4.Formulas;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IBlockFormulaExtension
{
    public static class IBlockFormulaMixin
    {
        public static string ParentBlockName = "ParentBlock";
        public static string ChildBlocksName = "ChildBlocks";
        public static string VariablesName = "Variables";
        public static string FormulasName = "Formulas";
        public static string ResultName = "Result";

        public static int ParentBlockIndex = INonterminalMixin.TypeIndex + 1;
        public static int ChildBlocksIndex = ParentBlockIndex + 1;
        public static int VariablesIndex = ChildBlocksIndex + 1;
        public static int FormulasIndex = VariablesIndex + 1;
        public static int ResultIndex = FormulasIndex + 1;

        public static ReadOnlyCollection<INode> ToReadOnlyProperties(
            ITerminal<ITypeDeclaration> type,
            IBlockFormula parentBlock,
            IBlockFormula childBlocks,
            INonterminal variables,
            INonterminal formulas,
            INonterminal result)
        {
            var list = (IList<INode>)new List<INode>();
            INonterminalMixin.InsertToProperties(new NodeType(NonterminalType.Block), type, ref list);
            list.Insert(ParentBlockIndex, parentBlock);
            list.Insert(ChildBlocksIndex, childBlocks);
            list.Insert(VariablesIndex, variables);
            list.Insert(FormulasIndex, formulas);
            list.Insert(ResultIndex, result);
            return new ReadOnlyCollection<INode>(list);
        }

        public static Collection<INodeRef> ToProperties(
            ITerminalRef<ITypeDeclaration> type,
            IBlockFormulaRef parentBlock,
            IBlockFormulaRef childBlocks,
            INonterminalRef variables,
            INonterminalRef formulas,
            INonterminalRef result)
        {
            var list = (IList<INodeRef>)new List<INodeRef>();
            INonterminalMixin.InsertToProperties(new NodeTypeRef(NonterminalType.Block), type, ref list);
            list.Insert(ParentBlockIndex, parentBlock);
            list.Insert(ChildBlocksIndex, childBlocks);
            list.Insert(VariablesIndex, variables);
            list.Insert(FormulasIndex, formulas);
            list.Insert(ResultIndex, result);
            return new Collection<INodeRef>(list);
        }

        public static IBlockFormula GetParentBlock(this IList<INode> source)
        {
            return (IBlockFormula)source[ParentBlockIndex];
        }

        public static IBlockFormulaRef SetParentBlock(this IList<INodeRef> source, IBlockFormulaRef value)
        {
            source[ParentBlockIndex] = value;
            return value;
        }

        public static IBlockFormula GetChildBlocks(this IList<INode> source)
        {
            return (IBlockFormula)source[ChildBlocksIndex];
        }

        public static IBlockFormulaRef SetChildBlocks(this IList<INodeRef> source, IBlockFormulaRef value)
        {
            source[ChildBlocksIndex] = value;
            return value;
        }

        public static INonterminal GetVariables(this IList<INode> source)
        {
            return (INonterminal)source[VariablesIndex];
        }

        public static INonterminalRef SetVariables(this IList<INodeRef> source, INonterminalRef value)
        {
            source[VariablesIndex] = value;
            return value;
        }

        public static INonterminal GetFormulas(this IList<INode> source)
        {
            return (INonterminal)source[FormulasIndex];
        }

        public static INonterminalRef SetFormulas(this IList<INodeRef> source, INonterminalRef value)
        {
            source[FormulasIndex] = value;
            return value;
        }

        public static INonterminal GetResult(this IList<INode> source)
        {
            return (INonterminal)source[ResultIndex];
        }

        public static INonterminalRef SetResult(this IList<INodeRef> source, INonterminalRef value)
        {
            source[ResultIndex] = value;
            return value;
        }
    }
}
