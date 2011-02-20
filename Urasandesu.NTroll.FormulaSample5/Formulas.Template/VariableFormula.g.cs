
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class VariableFormula : Formula
    {
        public VariableFormula()
            : this(default(string), default(bool))
        {
        }

        public VariableFormula(bool prohibitsInitProperties)
            : this(default(string), prohibitsInitProperties)
        {
        }

        public VariableFormula(string name, bool prohibitsInitProperties)
            : base(name, FormulaType.Variable)
        {
			if (!prohibitsInitProperties)
			{
	            Properties.Insert(VariableNameIndex, default(Node));
				VariableName = new Item<string>();
			}
        }
        public static readonly int VariableNameIndex =  TypeDeclarationIndex + 1; 		
        protected Item<string> GetVariableName() { return (Item<string>)Properties[VariableNameIndex]; }
        protected Item<string> SetVariableName(Item<string> value) { value.Name = "VariableName"; Properties[VariableNameIndex] = value; return value; }
        Item<string> variableName;
        public Item<string> VariableName { get { return variableName; } set { variableName = SetVariableName(CheckCanModify(value)); } }

    }
}

