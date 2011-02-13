using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public interface INonterminal : INode
    {
        ITerminal<NonterminalType> NodeType { get; }
        ITerminal<ITypeDeclaration> Type { get; }
        INonterminal Accept(INonterminalVisitor visitor);
    }

    public static class INonterminalMixin
    {
        public static string NodeTypeName = "NodeType";
        public static string TypeDeclarationName = "TypeDeclaration";

        public static readonly int NodeTypeIndex = 0;
        public static readonly int TypeIndex = NodeTypeIndex + 1;

        public static void InsertToProperties(ITerminal<NonterminalType> nodeType, ITerminal<ITypeDeclaration> type, ref IList<INode> list)
        {
            list.Insert(NodeTypeIndex, nodeType);
            list.Insert(TypeIndex, type);
        }

        public static void InsertToProperties(ITerminalRef<NonterminalType> nodeType, ITerminalRef<ITypeDeclaration> type, ref IList<INodeRef> list)
        {
            list.Insert(NodeTypeIndex, nodeType);
            list.Insert(TypeIndex, type);
        }

        public static ITerminal<NonterminalType> GetNodeType(this IList<INode> properties)
        {
            return (ITerminal<NonterminalType>)properties.ElementAtOrDefault(NodeTypeIndex) ?? default(ITerminal<NonterminalType>);
        }

        public static ITerminalRef<NonterminalType> GetNodeType(this IList<INodeRef> properties)
        {
            return (ITerminalRef<NonterminalType>)properties.ElementAtOrDefault(NodeTypeIndex) ?? default(ITerminalRef<NonterminalType>);
        }

        public static ITerminalRef<NonterminalType> SetNodeType(this INonterminal source, ITerminalRef<NonterminalType> value)
        {
            source[NodeTypeIndex] = value;
            return value;
        }

        public static ITerminal<ITypeDeclaration> GetTypeDeclaration(this IList<INode> properties)
        {
            return (ITerminal<ITypeDeclaration>)properties.ElementAtOrDefault(TypeIndex) ?? default(ITerminal<ITypeDeclaration>);
        }

        public static ITerminalRef<ITypeDeclaration> GetTypeDeclaration(this IList<INodeRef> properties)
        {
            return (ITerminalRef<ITypeDeclaration>)properties.ElementAtOrDefault(TypeIndex) ?? default(ITerminalRef<ITypeDeclaration>);
        }

        public static ITerminalRef<ITypeDeclaration> SetType(this INonterminal source, ITerminalRef<ITypeDeclaration> value)
        {
            source[TypeIndex] = value;
            return value;
        }

        public static void DumpToString(this INonterminal source, StringBuilder sb)
        {
            sb.Append("{");
            sb.Append("\"HashCode\": ");
            sb.Append(source.GetHashCode());
            sb.Append(", ");
            sb.Append("\"Referrers\": ");
            source.DoIfFirstHierarchy(() => sb.Append(source.Referrers.NullableToString()), () => sb.Append("\"Abbreviated ...\""));
            foreach (var property in source)
            {
                sb.Append(", ");
                sb.Append(property);
            }
            sb.Append("}");
        }
    }
}
