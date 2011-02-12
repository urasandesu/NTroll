using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public abstract class Formula : Node, IFormula
    {
        //protected internal Formula(IConstantNode<NodeType> nodeType, string name, IConstantNode<ITypeDeclaration> type, IFormula parent)
        //    : this(nodeType, name, type, parent, new INode[] { })
        //{
        //}

        protected internal Formula(IConstantNode<NodeType> nodeType, string name, IConstantNode<ITypeDeclaration> type, IFormula parent, params INode[] properties)
            : this(parent, name, new INode[] { type, parent }.Concat(properties).ToArray())
        {
        }

        Formula(IFormula parent, string name, IList<INode> properties)
            : base(parent, name, new NodeCollection<INode>(properties, parent))
        {
            NodeType = (IConstantNode<NodeType>)properties[0];
            Type = (IConstantNode<ITypeDeclaration>)properties[1];
        }

        public IConstantNode<NodeType> NodeType { get; private set; }
        public IConstantNode<ITypeDeclaration> Type { get; private set; }

        public IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }
    }

    public static class IFormulaMixin
    {
        public static void DumpToString(this IFormula source, StringBuilder sb)
        {
            sb.Append(source);
        }
    }
}
