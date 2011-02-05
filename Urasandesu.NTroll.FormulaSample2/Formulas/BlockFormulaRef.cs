using System;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Linq;
using System.Linq;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class BlockFormulaRef : BlockFormula, IBlockFormulaRef
    {
        protected internal BlockFormulaRef()
            : this(
                default(IBlockFormulaRef), 
                new Collection<IBlockFormulaRef>(), 
                new Collection<IFormulaRef>(), 
                new Collection<IFormulaRef>(), 
                default(IFormulaRef), 
                default(ITypeDeclaration), 
                default(IFormulaRef))
        {
        }

        protected internal BlockFormulaRef(
            IBlockFormulaRef parentBlock, 
            Collection<IBlockFormulaRef> childBlocks,
            Collection<IFormulaRef> variables, 
            Collection<IFormulaRef> formulas, 
            IFormulaRef result, 
            ITypeDeclaration type, 
            IFormulaRef parent)
            : base(
                parentBlock, 
                childBlocks.ToReadOnlyWithCast<IBlockFormula>(), 
                variables.ToReadOnlyWithCast<IFormula>(), 
                formulas.ToReadOnlyWithCast<IFormula>(), 
                result, 
                type, 
                parent)
        {
            this.parent = parent;
            this.parentBlock = parentBlock;
            this.childBlocks = childBlocks;
            this.variables = variables;
            this.formulas = formulas;
            this.result = result;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }


        IBlockFormulaRef parentBlock;
        public new IBlockFormulaRef ParentBlock { get { return parentBlock; } set { parentBlock = value; base.ParentBlock = (IBlockFormula)parentBlock; } }
        Collection<IBlockFormulaRef> childBlocks;
        public new Collection<IBlockFormulaRef> ChildBlocks { get { return childBlocks; } set { childBlocks = value; base.ChildBlocks = childBlocks.ToReadOnlyWithCast<IBlockFormula>(); } }
        Collection<IFormulaRef> variables;
        public new Collection<IFormulaRef> Variables { get { return variables; } set { variables = value; base.Variables = variables.ToReadOnlyWithCast<IFormula>(); } }
        Collection<IFormulaRef> formulas;
        public new Collection<IFormulaRef> Formulas { get { return formulas; } set { formulas = value; base.Formulas = formulas.ToReadOnlyWithCast<IFormula>(); } }
        IFormulaRef result;
        public new IFormulaRef Result { get { return result; } set { result = value; base.Result = (IFormula)result; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IBlockFormula Establish()
        {
            var established = default(IBlockFormula);
            FirstHierarchyOnly(() =>
            {
                var parentBlock = ParentBlock == null ? default(IBlockFormula) : ParentBlock.Establish();
                var childBlocks = ChildBlocks == null ? default(ReadOnlyCollection<IBlockFormula>) : ChildBlocks.Select(_ => _.Establish()).ToReadOnly();
                var variables = Variables == null ? default(ReadOnlyCollection<IFormula>) : Variables.Select(_ => _.Establish()).ToReadOnly();
                var formulas = Formulas == null ? default(ReadOnlyCollection<IFormula>) : Formulas.Select(_ => _.Establish()).ToReadOnly();
                var result = Result == null ? default(IFormula) : Result.Establish();
                var type = Type;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Block(parentBlock, childBlocks, variables, formulas, result, type, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
