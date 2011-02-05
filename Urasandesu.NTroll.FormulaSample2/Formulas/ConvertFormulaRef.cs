using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ConvertFormulaRef : ConvertFormula, IConvertFormulaRef
    {
        protected internal ConvertFormulaRef()
            : this(
                default(ITypeDeclaration), 
                default(IFormulaRef), 
                default(IMethodDeclaration), 
                default(IFormulaRef))
        {
        }

        protected internal ConvertFormulaRef(ITypeDeclaration type, IFormulaRef operand, IMethodDeclaration method, IFormulaRef parent)
            : base(type, operand, method, parent)
        {
            this.parent = parent;
            this.operand = operand;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        public new IMethodDeclaration Method { get { return base.Method; } set { base.Method = value; } }
        IFormulaRef operand;
        public new IFormulaRef Operand { get { return operand; } set { operand = value; base.Operand = operand; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IConvertFormula Establish()
        {
            var established = default(IConvertFormula);
            FirstHierarchyOnly(() =>
            {
                var type = Type;
                var operand = Operand == null ? default(IFormula) : Operand.Establish();
                var method = Method;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Convert(type, operand, method, parent);
            });
            return established;
        }

        IUnaryFormula IUnaryFormulaRef.Establish()
        {
            return Establish();
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
