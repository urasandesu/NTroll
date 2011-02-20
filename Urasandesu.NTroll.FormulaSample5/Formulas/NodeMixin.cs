using System.Collections.Generic;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public static class NodeMixin
    {
        public static void AppendListContentTo<TNode>(this IList<TNode> nodes, StringBuilder sb)
            where TNode : Node
        {
            sb.Append("[");
            var oneOrMore = false;
            foreach (var node in nodes)
            {
                if (!oneOrMore)
                {
                    oneOrMore = true;
                    node.AppendTo(sb);
                }
                else
                {
                    sb.Append(", ");
                    node.AppendTo(sb);
                }
            }
            sb.Append("]");
        }
    }
}
