using System;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConstantFormulaRef : ConstantFormula, IConstantFormulaRef
    {
        protected internal ConstantFormulaRef()
            : this(null, null, null)
        {
        }

        protected internal ConstantFormulaRef(object value, ITypeDeclaration type, IFormulaRef parent)
            : base(value, type, parent)
        {
            this.Value = value;
            this.parent = parent;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        public new object Value { get { return base.Value; } set { base.Value = value; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IConstantFormula Establish()
        {
            var established = default(IConstantFormula);
            FirstHierarchyOnly(() =>
            {
                var value = Value;
                var type = Type;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Constant(value, type, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
