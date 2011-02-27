using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConditionalFormula : Formula
    {
        public ConditionalFormula()
            : base()
        {
            NodeType = NodeType.Conditional;
            Test = default(Formula);
            IfTrue = default(Formula);
            IfFalse = default(Formula);
            Initialize();
        }

        public const string NameOfTest = "Test";
        Formula test;
        public Formula Test 
        { 
            get { return test; } 
            set 
            {
                SetFormula(NameOfTest, value, ref test);
            }
        }
        public const string NameOfIfTrue = "IfTrue";
        Formula ifTrue;
        public Formula IfTrue 
        { 
            get { return ifTrue; } 
            set 
            {
                SetFormula(NameOfIfTrue, value, ref ifTrue);
            }
        }
        public const string NameOfIfFalse = "IfFalse";
        Formula ifFalse;
        public Formula IfFalse 
        { 
            get { return ifFalse; } 
            set 
            {
                SetFormula(NameOfIfFalse, value, ref ifFalse);
            }
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            Test = Formula.Pin(Test);
            IfTrue = Formula.Pin(IfTrue);
            IfFalse = Formula.Pin(IfFalse);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfTest);
            sb.Append("\": ");
            if (Test == null)
            {
                sb.Append("null");
            }
            else
            {
                Test.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfIfTrue);
            sb.Append("\": ");
            if (IfTrue == null)
            {
                sb.Append("null");
            }
            else
            {
                IfTrue.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfIfFalse);
            sb.Append("\": ");
            if (IfFalse == null)
            {
                sb.Append("null");
            }
            else
            {
                IfFalse.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

