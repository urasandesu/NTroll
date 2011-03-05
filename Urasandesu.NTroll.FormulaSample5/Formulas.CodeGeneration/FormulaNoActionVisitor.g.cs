using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class FormulaNoActionVisitor : IFormulaVisitor
    {
        public Formula Visit(BinaryFormula formula)
        {
            return formula;
        }
        public Formula Visit(AssignFormula formula)
        {
            return formula;
        }
        public Formula Visit(NotEqualFormula formula)
        {
            return formula;
        }
        public Formula Visit(AddFormula formula)
        {
            return formula;
        }
        public Formula Visit(MultiplyFormula formula)
        {
            return formula;
        }
        public Formula Visit(AndAlsoFormula formula)
        {
            return formula;
        }
        public Formula Visit(EqualFormula formula)
        {
            return formula;
        }
        public Formula Visit(ExclusiveOrFormula formula)
        {
            return formula;
        }
        public Formula Visit(BlockFormula formula)
        {
            return formula;
        }
        public Formula Visit(ConstantFormula formula)
        {
            return formula;
        }
        public Formula Visit(Formula formula)
        {
            return formula;
        }
        public Formula Visit(VariableFormula formula)
        {
            return formula;
        }
        public Formula Visit(UnaryFormula formula)
        {
            return formula;
        }
        public Formula Visit(ConvertFormula formula)
        {
            return formula;
        }
        public Formula Visit(TypeAsFormula formula)
        {
            return formula;
        }
        public Formula Visit(ConditionalFormula formula)
        {
            return formula;
        }
        public Formula Visit(ReturnFormula formula)
        {
            return formula;
        }
        public Formula Visit(CallFormula formula)
        {
            return formula;
        }
        public Formula Visit(ReflectiveCallFormula formula)
        {
            return formula;
        }
        public Formula Visit(NewArrayInitFormula formula)
        {
            return formula;
        }
        public Formula Visit(NewFormula formula)
        {
            return formula;
        }
        public Formula Visit(ReflectiveNewFormula formula)
        {
            return formula;
        }
        public Formula Visit(MemberFormula formula)
        {
            return formula;
        }
        public Formula Visit(PropertyFormula formula)
        {
            return formula;
        }
        public Formula Visit(ReflectivePropertyFormula formula)
        {
            return formula;
        }
        public Formula Visit(FieldFormula formula)
        {
            return formula;
        }
        public Formula Visit(ReflectiveFieldFormula formula)
        {
            return formula;
        }
        public Formula Visit(EndFormula formula)
        {
            return formula;
        }
    }
}
