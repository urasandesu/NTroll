﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class ExclusiveOrFormula : BinaryFormula, IExclusiveOrFormula
    {
        protected internal ExclusiveOrFormula(IFormula left, IFormula right, ITypeDeclaration type, IMethodDeclaration method, IFormula parent)
            : base(NodeType.ExclusiveOr, left, right, type, method, parent)
        {
        }

        protected override void AppendMethodToString(StringBuilder sb)
        {
            if (Method == null)
            {
                sb.Append("\"^\"");
            }
            else
            {
                sb.Append("\"");
                sb.Append(Method.ToString());
                sb.Append("\"");
            }
        }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}