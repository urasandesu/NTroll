using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class BlockFormula : Formula
    {
        public BlockFormula()
            : base()
        {
            NodeType = NodeType.Block;
            ParentBlock = default(BlockFormula);
            ChildBlocks = new FormulaCollection<BlockFormula>();
            Variables = new FormulaCollection<Formula>();
            Formulas = new FormulaCollection<Formula>();
            Result = default(Formula);
        }

		public const string NameOfParentBlock = "ParentBlock";
        BlockFormula parentBlock;
        public BlockFormula ParentBlock 
        { 
            get { return parentBlock; } 
            set { parentBlock = CheckCanModify(value); OnPropertyChanged(NameOfParentBlock); } 
        }
		public const string NameOfChildBlocks = "ChildBlocks";
        FormulaCollection<BlockFormula> childBlocks;
        public FormulaCollection<BlockFormula> ChildBlocks 
        { 
            get { return childBlocks; } 
            set { childBlocks = CheckCanModify(value); OnPropertyChanged(NameOfChildBlocks); } 
        }
		public const string NameOfVariables = "Variables";
        FormulaCollection<Formula> variables;
        public FormulaCollection<Formula> Variables 
        { 
            get { return variables; } 
            set { variables = CheckCanModify(value); OnPropertyChanged(NameOfVariables); } 
        }
		public const string NameOfFormulas = "Formulas";
        FormulaCollection<Formula> formulas;
        public FormulaCollection<Formula> Formulas 
        { 
            get { return formulas; } 
            set { formulas = CheckCanModify(value); OnPropertyChanged(NameOfFormulas); } 
        }
		public const string NameOfResult = "Result";
        Formula result;
        public Formula Result 
        { 
            get { return result; } 
            set { result = CheckCanModify(value); OnPropertyChanged(NameOfResult); } 
        }


        public override Formula Accept(IFormulaVisitor visitor)
        {
            return visitor.Visit(this);
        }


        protected override Formula PinCore()
        {
            ParentBlock = (BlockFormula)Formula.Pin(ParentBlock);
            ChildBlocks = (FormulaCollection<BlockFormula>)Formula.Pin(ChildBlocks);
            Variables = (FormulaCollection<Formula>)Formula.Pin(Variables);
            Formulas = (FormulaCollection<Formula>)Formula.Pin(Formulas);
            Result = Formula.Pin(Result);
            return base.PinCore();
        }


        public override void AppendTo(StringBuilder sb)
        {
            sb.Append("{");
            base.AppendTo(sb);
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfChildBlocks);
            sb.Append("\": ");
            if (ChildBlocks == null)
            {
                sb.Append("null");
            }
            else
            {
                ChildBlocks.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfVariables);
            sb.Append("\": ");
            if (Variables == null)
            {
                sb.Append("null");
            }
            else
            {
                Variables.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfFormulas);
            sb.Append("\": ");
            if (Formulas == null)
            {
                sb.Append("null");
            }
            else
            {
                Formulas.AppendTo(sb);
            }
            sb.Append(", ");
            sb.Append("\"");
            sb.Append(NameOfResult);
            sb.Append("\": ");
            if (Result == null)
            {
                sb.Append("null");
            }
            else
            {
                Result.AppendTo(sb);
            }
            sb.Append("}");
        }
    }
}

