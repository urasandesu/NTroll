using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class ConvertIncreaser : FormulaAdapter
    {
        public ConvertIncreaser(IFormulaVisitor visitor)
            : base(visitor)
        {
        }

        public override Formula Visit(BinaryFormula formula)
        {
            IncreaseIfNecessary(formula.Right, _ => _.TypeDeclaration, increased => formula.Right = increased);
            return base.Visit(formula);
        }

        public override Formula Visit(UnaryFormula formula)
        {
            IncreaseIfNecessary(formula.Operand, _ => _.TypeDeclaration, increased => formula.Operand = increased);
            return base.Visit(formula);
        }

        public override Formula Visit(ConditionalFormula formula)
        {
            IncreaseIfNecessary(formula.Test, _ => _.TypeDeclaration, increased => formula.Test = increased);
            return base.Visit(formula);
        }

        public override Formula Visit(ReturnFormula formula)
        {
            IncreaseIfNecessary(formula.Body, _ => _.TypeDeclaration, increased => formula.Body = increased);
            return base.Visit(formula);
        }

        public override Formula Visit(CallFormula formula)
        {
            IncreaseIfNecessary(formula.Instance, _ => _.TypeDeclaration, increased => formula.Instance = increased);
            return base.Visit(formula);
        }

        public override Formula Visit(NewArrayInitFormula formula)
        {
            var expectedType = formula.TypeDeclaration.GetElementType();
            for (int i = 0; i < formula.Formulas.Count; i++)
            {
                IncreaseIfNecessary(formula.Formulas[i], _ => expectedType, increased => formula.Formulas[i] = increased);
            }
            return base.Visit(formula);
        }

        public override Formula Visit(MemberFormula formula)
        {
            IncreaseIfNecessary(formula.Instance, _ => _.TypeDeclaration, increased => formula.Instance = increased);
            return base.Visit(formula);
        }

        void IncreaseIfNecessary(Formula formula, Func<Formula, ITypeDeclaration> getExpectedType, Action<Formula> increase)
        {
            if (formula != null)
            {
                var expectedType = getExpectedType(formula);
                if (!expectedType.IsValueType && formula.TypeDeclaration.IsValueType && expectedType.IsAssignableFrom(formula.TypeDeclaration))
                {
                    var convert = new ConvertFormula(formula, expectedType);
                    increase(convert);
                }
            }
        }
    }
}
