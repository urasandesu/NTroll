using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConditionalFormulaRef : ConditionalFormula, IConditionalFormulaRef
    {
        protected internal ConditionalFormulaRef()
            : this(
                default(IFormulaRef),
                default(IFormulaRef),
                default(IFormulaRef),
                default(ITypeDeclaration),
                default(IFormulaRef))
        {
        }

        protected internal ConditionalFormulaRef(
            IFormulaRef test,
            IFormulaRef ifTrue,
            IFormulaRef ifFalse,
            ITypeDeclaration type,
            IFormulaRef parent)
            : base(test, ifTrue, ifFalse, type, parent)
        {
            this.parent = parent;
            this.test = test;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        IFormulaRef test;
        public new IFormulaRef Test { get { return test; } set { test = value; base.Test = test; } }
        IFormulaRef ifTrue;
        public new IFormulaRef IfTrue { get { return ifTrue; } set { ifTrue = value; base.IfTrue = ifTrue; } }
        IFormulaRef ifFalse;
        public new IFormulaRef IfFalse { get { return ifFalse; } set { ifFalse = value; base.IfFalse = ifFalse; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IConditionalFormula Establish()
        {
            var established = default(IConditionalFormula);
            FirstHierarchyOnly(() =>
            {
                var test = Test == null ? default(IFormula) : Test.Establish();
                var ifTrue = IfTrue == null ? default(IFormula) : IfTrue.Establish();
                var ifFalse = IfFalse == null ? default(IFormula) : IfFalse.Establish();
                var type = Type;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Condition(test, ifTrue, ifFalse, type, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
