using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract class FormulaAdapter : IFormulaVisitor
    {
        IFormulaVisitor visitor;
        public FormulaAdapter(IFormulaVisitor visitor)
        {
            this.visitor = visitor;
        }
        public virtual Formula Visit(BinaryFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(AssignFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(NotEqualFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(AddFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(MultiplyFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(AndAlsoFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(EqualFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(ExclusiveOrFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(BlockFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(ConstantFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(Formula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(VariableFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(UnaryFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(ConvertFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(TypeAsFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(ConditionalFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(ReturnFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(CallFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(NewArrayInitFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(NewFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(MemberFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(PropertyFormula formula)
        {
            return visitor.Visit(formula);
        }
        public virtual Formula Visit(FieldFormula formula)
        {
            return visitor.Visit(formula);
        }
    }
}
