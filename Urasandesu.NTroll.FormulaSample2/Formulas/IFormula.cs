using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IFormula
    {
        NodeType NodeType { get; }
        ITypeDeclaration Type { get; }
        IFormula Parent { get; }
        IFormula Accept(IFormulaVisitor visitor);
    }
}
