using System;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ReturnFormulaRef : ReturnFormula, IReturnFormulaRef
    {
        protected internal ReturnFormulaRef()
            : this(default(IFormulaRef), default(ITypeDeclaration), default(IFormulaRef))
        {
        }

        protected internal ReturnFormulaRef(IFormulaRef @object, ITypeDeclaration type, IFormulaRef parent)
            : base(@object, type, parent)
        {
            this.parent = parent;
            this.@object = @object;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        IFormulaRef @object;
        public new IFormulaRef Object { get { return @object; } set { @object = value; base.Object = @object; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IReturnFormula Establish()
        {
            var established = default(IReturnFormula);
            FirstHierarchyOnly(() =>
            {
                var @object = Object == null ? default(IFormula) : Object.Establish();
                var type = Type;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Return(@object, type, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
