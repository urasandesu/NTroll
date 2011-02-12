using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public class ConstantNode<TValue> : Node, IConstantNode<TValue>
    {
        public ConstantNode(string name)
            : this(name, default(TValue))
        {
        }

        public ConstantNode(string name, TValue value)
            : base(default(INode), name, default(INodeCollection<INode>))
        {
            Value = value;
        }

        public TValue Value { get; protected internal set; }

        public override void ContentToString(StringBuilder sb)
        {
            this.DumpToString(sb);
        }
    }

    public static class IConstantNodeMixin
    {
        public static void DumpToString<TValue>(this IConstantNode<TValue> source, StringBuilder sb)
        {
            sb.Append(source.Value);
        }
    }

    public class NodeTypeConstant : ConstantNode<NodeType>
    {
        public NodeTypeConstant()
            : this(NodeType.None)
        {
        }

        public NodeTypeConstant(NodeType nodeType)
            : base("NodeType", nodeType)
        {
        }
    }

    public class NodeTypeConstantRef : ConstantNodeRef<NodeType>
    {
        public NodeTypeConstantRef()
            : this(NodeType.None)
        {
        }

        public NodeTypeConstantRef(NodeType nodeType)
            : base("NodeType", nodeType)
        {
        }
    }

    public class TypeConstant : ConstantNode<ITypeDeclaration>
    {
        public TypeConstant()
            : this(null)
        {
        }

        public TypeConstant(ITypeDeclaration type)
            : base("Type", type)
        {
        }
    }

    public class TypeConstantRef : ConstantNodeRef<ITypeDeclaration>
    {
        public TypeConstantRef()
            : this(null)
        {
        }

        public TypeConstantRef(ITypeDeclaration type)
            : base("Type", type)
        {
        }
    }
}
