using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class AddFormulaRef : AddFormula, IAddFormulaRef
    {
        protected internal AddFormulaRef()
            : this(default(IFormulaRef), default(IFormulaRef), default(ITypeDeclaration), default(IMethodDeclaration), default(IFormulaRef))
        {
        }

        protected internal AddFormulaRef(IFormulaRef left, IFormulaRef right, ITypeDeclaration type, IMethodDeclaration method, IFormulaRef parent)
            : base(left, right, type, method, parent)
        {
            this.parent = parent;
            this.left = left;
            this.right = right;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        IFormulaRef left;
        public new IFormulaRef Left { get { return left; } set { left = value; base.Left = (IFormula)left; } }
        public new IMethodDeclaration Method { get { return base.Method; } set { base.Method = value; } }
        IFormulaRef right;
        public new IFormulaRef Right { get { return right; } set { right = value; base.Right = (IFormula)right; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IAddFormula Establish()
        {
            var established = default(IAddFormula);
            FirstHierarchyOnly(() =>
            {
                var left = Left == null ? default(IFormula) : Left.Establish();
                var right = Right == null ? default(IFormula) : Right.Establish();
                var type = Type;
                var method = Method;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Add(left, right, type, method, parent);
            });
            return established;
        }

        IBinaryFormula IBinaryFormulaRef.Establish()
        {
            return Establish();
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
