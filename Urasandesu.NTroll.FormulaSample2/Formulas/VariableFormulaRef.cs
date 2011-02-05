using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class VariableFormulaRef : VariableFormula, IVariableFormulaRef
    {
        protected internal VariableFormulaRef()
            : this(null, null, null)
        {
        }

        protected internal VariableFormulaRef(ITypeDeclaration type, string name, IFormulaRef parent)
            : base(type, name, parent)
        {
            this.parent = parent;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        public new string Name { get { return base.Name; } set { base.Name = value; } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IVariableFormula Establish()
        {
            var established = default(IVariableFormula);
            FirstHierarchyOnly(() =>
            {
                var type = Type;
                var name = Name;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Variable(type, name, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
