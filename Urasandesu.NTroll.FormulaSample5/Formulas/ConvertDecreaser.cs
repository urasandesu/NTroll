using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class ConvertDecreaser : FormulaAdapter
    {
        public ConvertDecreaser(IFormulaVisitor visitor)
            : base(visitor)
        {
        }

        public override Formula Visit(BinaryFormula formula)
        {
            DecreaseIfNecessary(formula.Right, _ => _.TypeDeclaration, decreased => formula.Right = decreased);
            return base.Visit(formula);
        }

        public override Formula Visit(UnaryFormula formula)
        {
            DecreaseIfNecessary(formula.Operand, _ => _.TypeDeclaration, decreased => formula.Operand = decreased);
            return base.Visit(formula);
        }

        public override Formula Visit(ConditionalFormula formula)
        {
            DecreaseIfNecessary(formula.Test, _ => _.TypeDeclaration, decreased => formula.Test = decreased);
            return base.Visit(formula);
        }

        public override Formula Visit(ReturnFormula formula)
        {
            DecreaseIfNecessary(formula.Body, _ => _.TypeDeclaration, decreased => formula.Body = decreased);
            return base.Visit(formula);
        }

        public override Formula Visit(CallFormula formula)
        {
            DecreaseIfNecessary(formula.Instance, _ => _.TypeDeclaration, decreased => formula.Instance = decreased);
            return base.Visit(formula);
        }

        public override Formula Visit(NewArrayInitFormula formula)
        {
            var expectedType = formula.TypeDeclaration.GetElementType();
            for (int i = 0; i < formula.Formulas.Count; i++)
            {
                DecreaseIfNecessary(formula.Formulas[i], _ => expectedType, decreased => formula.Formulas[i] = decreased);
            }
            return base.Visit(formula);
        }

        public override Formula Visit(MemberFormula formula)
        {
            DecreaseIfNecessary(formula.Instance, _ => _.TypeDeclaration, decreased => formula.Instance = decreased);
            return base.Visit(formula);
        }

        void DecreaseIfNecessary(Formula formula, Func<Formula, ITypeDeclaration> getExpectedType, Action<Formula> decrease)
        {
            if (formula != null)
            {
                if (formula.NodeType == NodeType.Convert)
                {
                    var expectedType = getExpectedType(formula);
                    var operand = ((ConvertFormula)formula).Operand;
                    if ((expectedType.IsValueType || !operand.TypeDeclaration.IsValueType) && expectedType.IsAssignableFrom(operand.TypeDeclaration))
                    {
                        decrease(operand);
                    }
                }
            }
        }
    }
}
