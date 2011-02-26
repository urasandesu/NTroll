using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

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
        }

		public const string NameOfTest = "Test";
        Formula test;
        public Formula Test 
        { 
            get { return test; } 
            set { test = CheckCanModify(value); OnPropertyChanged(NameOfTest); } 
        }
		public const string NameOfIfTrue = "IfTrue";
        Formula ifTrue;
        public Formula IfTrue 
        { 
            get { return ifTrue; } 
            set { ifTrue = CheckCanModify(value); OnPropertyChanged(NameOfIfTrue); } 
        }
		public const string NameOfIfFalse = "IfFalse";
        Formula ifFalse;
        public Formula IfFalse 
        { 
            get { return ifFalse; } 
            set { ifFalse = CheckCanModify(value); OnPropertyChanged(NameOfIfFalse); } 
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

