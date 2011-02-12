
using System.Text;
namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface INode
    {
        INode Parent { get; }
        string Name { get; }
        INodeCollection<INode> Children { get; }
        void ContentToString(StringBuilder sb);
    }
}
