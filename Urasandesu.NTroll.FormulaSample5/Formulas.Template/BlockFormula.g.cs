
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
            : this(default(string), default(bool))
        {
        }

        public BlockFormula(bool prohibitsInitProperties)
            : this(default(string), prohibitsInitProperties)
        {
        }

        public BlockFormula(string name, bool prohibitsInitProperties)
            : base(name, FormulaType.Block)
        {
			if (!prohibitsInitProperties)
			{
	            Properties.Insert(ParentBlockIndex, default(Node));
				ParentBlock = new NullBlockFormula();
	            Properties.Insert(ChildBlocksIndex, default(Node));
				ChildBlocks = new FormulaCollection<BlockFormula>();
	            Properties.Insert(VariablesIndex, default(Node));
				Variables = new FormulaCollection<Formula>();
	            Properties.Insert(FormulasIndex, default(Node));
				Formulas = new FormulaCollection<Formula>();
	            Properties.Insert(ResultIndex, default(Node));
				Result = new NullFormula();
			}
        }
        public static readonly int ParentBlockIndex =  TypeDeclarationIndex + 1; 		
        protected BlockFormula GetParentBlock() { return (BlockFormula)Properties[ParentBlockIndex]; }
        protected BlockFormula SetParentBlock(BlockFormula value) { value.Name = "ParentBlock"; Properties[ParentBlockIndex] = value; return value; }
        BlockFormula parentBlock;
        public BlockFormula ParentBlock { get { return parentBlock; } set { parentBlock = SetParentBlock(CheckCanModify(value)); } }

        public static readonly int ChildBlocksIndex =  ParentBlockIndex + 1; 		
        protected FormulaCollection<BlockFormula> GetChildBlocks() { return (FormulaCollection<BlockFormula>)Properties[ChildBlocksIndex]; }
        protected FormulaCollection<BlockFormula> SetChildBlocks(FormulaCollection<BlockFormula> value) { value.Name = "ChildBlocks"; Properties[ChildBlocksIndex] = value; return value; }
        FormulaCollection<BlockFormula> childBlocks;
        public FormulaCollection<BlockFormula> ChildBlocks { get { return childBlocks; } set { childBlocks = SetChildBlocks(CheckCanModify(value)); } }

        public static readonly int VariablesIndex =  ChildBlocksIndex + 1; 		
        protected FormulaCollection<Formula> GetVariables() { return (FormulaCollection<Formula>)Properties[VariablesIndex]; }
        protected FormulaCollection<Formula> SetVariables(FormulaCollection<Formula> value) { value.Name = "Variables"; Properties[VariablesIndex] = value; return value; }
        FormulaCollection<Formula> variables;
        public FormulaCollection<Formula> Variables { get { return variables; } set { variables = SetVariables(CheckCanModify(value)); } }

        public static readonly int FormulasIndex =  VariablesIndex + 1; 		
        protected FormulaCollection<Formula> GetFormulas() { return (FormulaCollection<Formula>)Properties[FormulasIndex]; }
        protected FormulaCollection<Formula> SetFormulas(FormulaCollection<Formula> value) { value.Name = "Formulas"; Properties[FormulasIndex] = value; return value; }
        FormulaCollection<Formula> formulas;
        public FormulaCollection<Formula> Formulas { get { return formulas; } set { formulas = SetFormulas(CheckCanModify(value)); } }

        public static readonly int ResultIndex =  FormulasIndex + 1; 		
        protected Formula GetResult() { return (Formula)Properties[ResultIndex]; }
        protected Formula SetResult(Formula value) { value.Name = "Result"; Properties[ResultIndex] = value; return value; }
        Formula result;
        public Formula Result { get { return result; } set { result = SetResult(CheckCanModify(value)); } }

    }
}

