using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public interface IFormulaVisitor
    {
        Formula Visit(BinaryFormula formula);
        Formula Visit(AssignFormula formula);
        Formula Visit(NotEqualFormula formula);
        Formula Visit(AddFormula formula);
        Formula Visit(MultiplyFormula formula);
        Formula Visit(AndAlsoFormula formula);
        Formula Visit(EqualFormula formula);
        Formula Visit(ExclusiveOrFormula formula);
        Formula Visit(BlockFormula formula);
        Formula Visit(ConstantFormula formula);
        Formula Visit(Formula formula);
        Formula Visit(VariableFormula formula);
        Formula Visit(UnaryFormula formula);
        Formula Visit(ConvertFormula formula);
        Formula Visit(TypeAsFormula formula);
        Formula Visit(ConditionalFormula formula);
        Formula Visit(ReturnFormula formula);
    }
}
