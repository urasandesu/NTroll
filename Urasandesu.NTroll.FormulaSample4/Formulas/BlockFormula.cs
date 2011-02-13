using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NTroll.FormulaSample4.Mixins.Urasandesu.NAnonym.ILTools.Formulas.IBlockFormulaExtension;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class BlockFormula : Nonterminal, IBlockFormula
    {
        protected internal BlockFormula(
            string name,
            INode referrers,
            ITerminal<ITypeDeclaration> type,
            IBlockFormula parentBlock,
            IBlockFormula childBlocks,
            INonterminal variables,
            INonterminal formulas,
            INonterminal result)
            : this(
                name,
                referrers,
                IBlockFormulaMixin.ToReadOnlyProperties(type, parentBlock, childBlocks, variables, formulas, result))
        {
        }

        protected internal BlockFormula(string name, INode referrers, ReadOnlyCollection<INode> properties)
            : base(name, referrers, properties)
        {
            ParentBlock = properties.GetParentBlock();
            ChildBlocks = properties.GetChildBlocks();
            Variables = properties.GetVariables();
            Formulas = properties.GetFormulas();
            Result = properties.GetResult();
        }

        public IBlockFormula ParentBlock { get; private set; }
        public IBlockFormula ChildBlocks { get; private set; }
        public INonterminal Variables { get; private set; }
        public INonterminal Formulas { get; private set; }
        public INonterminal Result { get; private set; }

        public override INonterminal Accept(INonterminalVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}
