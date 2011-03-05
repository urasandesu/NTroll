using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class ConvertReducer : FormulaAdapter
    {
        public ConvertReducer(IFormulaVisitor visitor)
            : base(visitor)
        {
        }

        public override Formula Visit(BinaryFormula formula)
        {
            Reduce(formula.Right, newRight => formula.Right = newRight);
            return base.Visit(formula);
        }

        public override Formula Visit(UnaryFormula formula)
        {
            Reduce(formula.Operand, newOperand => formula.Operand = newOperand);
            return base.Visit(formula);
        }

        public override Formula Visit(ConditionalFormula formula)
        {
            Reduce(formula.Test, newTest => formula.Test = newTest);
            return base.Visit(formula);
        }

        public override Formula Visit(ReturnFormula formula)
        {
            Reduce(formula.Body, newBody => formula.Body = newBody);
            return base.Visit(formula);
        }

        public override Formula Visit(CallFormula formula)
        {
            Reduce(formula.Instance, newInstance => formula.Instance = newInstance);
            return base.Visit(formula);
        }

        public override Formula Visit(MemberFormula formula)
        {
            Reduce(formula.Instance, newInstance => formula.Instance = newInstance);
            return base.Visit(formula);
        }

        void Reduce(Formula formula, Action<Formula> callbackIfReduce)
        {
            if (formula != null)
            {
                if (formula.NodeType == NodeType.Convert)
                {
                    var convert = (ConvertFormula)formula;
                    if (convert.TypeDeclaration.IsAssignableFrom(convert.Operand.TypeDeclaration))
                    {
                        Console.WriteLine("Reduce できたよー");
                        callbackIfReduce(convert.Operand);
                    }
                }
            }
        }
    }
}
