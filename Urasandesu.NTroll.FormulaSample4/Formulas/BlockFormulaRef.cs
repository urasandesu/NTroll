using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IBlockFormulaExtension;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class BlockFormulaRef : NonterminalRef, IBlockFormulaRef
    {
        protected internal BlockFormulaRef(
            string name,
            INodeRef referrers,
            ITerminalRef<ITypeDeclaration> type,
            IBlockFormulaRef parentBlock,
            IBlockFormulaRef childBlocks,
            INonterminalRef variables,
            INonterminalRef formulas,
            INonterminalRef result)
            : base(
                name,
                referrers,
                IBlockFormulaMixin.ToProperties(type, parentBlock, childBlocks, variables, formulas, result))
        {
            this.parentBlock = parentBlock;
            this.childBlocks = childBlocks;
            this.variables = variables;
            this.formulas = formulas;
            this.result = result;
        }

        IBlockFormulaRef parentBlock;
        public IBlockFormulaRef ParentBlock { get { return parentBlock; } set { parentBlock = this.SetParentBlock(value); } }
        IBlockFormulaRef childBlocks;
        public IBlockFormulaRef ChildBlocks { get { return childBlocks; } set { childBlocks = this.SetChildBlocks(value); } }
        INonterminalRef variables;
        public INonterminalRef Variables { get { return variables; } set { variables = this.SetVariables(value); } }
        INonterminalRef formulas;
        public INonterminalRef Formulas { get { return formulas; } set { formulas = this.SetFormulas(value); } }
        INonterminalRef result;
        public INonterminalRef Result { get { return result; } set { result = this.SetResult(value); } }

        IBlockFormula IBlockFormula.ParentBlock { get { return ParentBlock; } }
        IBlockFormula IBlockFormula.ChildBlocks { get { return ChildBlocks; } }
        INonterminal IBlockFormula.Variables { get { return Variables; } }
        INonterminal IBlockFormula.Formulas { get { return Formulas; } }
        INonterminal IBlockFormula.Result { get { return Result; } }

        public new IBlockFormula Pin()
        {
            return (IBlockFormula)base.Pin();
        }

        protected override INonterminal PinCore(string name, INode referrers, ReadOnlyCollection<INode> properties)
        {
            return new BlockFormula(name, referrers, properties);
        }

    }
}
