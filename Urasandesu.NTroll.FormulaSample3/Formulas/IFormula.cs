using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample3.Formulas
{
    public interface IFormula : INode
    {
        IConstantNode<NodeType> NodeType { get; }
        IConstantNode<ITypeDeclaration> Type { get; }
        IFormula Accept(IFormulaVisitor visitor);
    }
}
