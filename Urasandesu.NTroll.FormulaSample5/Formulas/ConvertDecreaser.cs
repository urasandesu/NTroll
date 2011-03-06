/* 
 * File: ConvertDecreaser.cs
 * 
 * Author: Akira Sugiura (urasandesu@gmail.com)
 * 
 * 
 * Copyright (c) 2010 Akira Sugiura
 *  
 *  This software is MIT License.
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy
 *  of this software and associated documentation files (the "Software"), to deal
 *  in the Software without restriction, including without limitation the rights
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *  copies of the Software, and to permit persons to whom the Software is
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 *  THE SOFTWARE.
 */
 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;

namespace Urasandesu.NAnonym.Formulas
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

