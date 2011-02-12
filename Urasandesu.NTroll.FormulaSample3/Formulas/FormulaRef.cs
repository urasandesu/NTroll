using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public abstract class FormulaRef : NodeRef, IFormulaRef
    {
        protected internal FormulaRef(IConstantNodeRef<NodeType> nodeType, string name, IConstantNodeRef<ITypeDeclaration> type, IFormulaRef parent, params INodeRef[] properties)
            : this(parent, name, new INodeRef[] { type, parent }.Concat(properties).ToArray())
        {
        }

        FormulaRef(IFormulaRef parent, string name, IList<INodeRef> properties)
            : base(parent, name, new NodeRefCollection<INode, INodeRef>(properties, parent))
        {
            nodeType = (IConstantNodeRef<NodeType>)properties[0];
            type = (IConstantNodeRef<ITypeDeclaration>)properties[1];
        }

        IConstantNodeRef<NodeType> nodeType;
        public IConstantNodeRef<NodeType> NodeType { get { return nodeType; } set { nodeType = value; base.Children[0] = nodeType; } }
        IConstantNodeRef<ITypeDeclaration> type;
        public IConstantNodeRef<ITypeDeclaration> Type { get { return type; } set { type = value; base.Children[1] = type; } }

        IConstantNode<NodeType> IFormula.NodeType { get { return NodeType; } }
        IConstantNode<ITypeDeclaration> IFormula.Type { get { return Type; } }

        public IFormula Accept(IFormulaVisitor visitor)
        {
            throw new InvalidOperationException("Must convert this to an object implemented IFormula by calling Pin().");
        }

        public new IFormula Pin()
        {
            return (IFormula)base.Pin();
        }

        protected override INode Pin(INode pinnedParent, INodeCollection<INode> pinnedChildren)
        {
            return Pin((IFormula)pinnedParent, pinnedChildren);
        }

        protected abstract IFormula Pin(IFormula parent, IList<INode> properties);

        public override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }

        public static IBlockFormulaRef Block(IBlockFormulaRef parentBlock)
        {
            throw new NotImplementedException();
        }
    }

    //public class BlockFormula : Formula, IBlockFormula
    //{
    //    protected internal BlockFormula(
    //        string name,
    //        IBlockFormula parentBlock,
    //        INodeCollection<IBlockFormula> childBlocks,
    //        INodeCollection<IFormula> variables,
    //        INodeCollection<IFormula> formulas,
    //        IFormula result,
    //        IConstantNode<ITypeDeclaration> type,
    //        IFormula parent) :
    //        base(
    //            new NodeTypeConstant(NodeType.Block),
    //            name,
    //            type,
    //            parent,
    //            parentBlock,
    //            childBlocks,
    //            variables,
    //            formulas,
    //            result)
    //    {
    //    }

    //    public IBlockFormula ParentBlock { get; private set; }
    //    public INodeCollection<IBlockFormula> ChildBlocks { get; private set; }
    //    public INodeCollection<IFormula> Variables { get; private set; }
    //    public INodeCollection<IFormula> Formulas { get; private set; }
    //    public IFormula Result { get; private set; }
    //}
}
