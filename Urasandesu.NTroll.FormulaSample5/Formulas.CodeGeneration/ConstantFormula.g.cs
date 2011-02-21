using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class ConstantFormula : Formula
    {
        public ConstantFormula()
            : this(default(bool))
        {
        }

        public ConstantFormula(bool prohibitsInitProperties)
            : base()
        {
			NodeType.Value = FormulaType.Constant;
            Properties.Insert(ConstantValueIndex, default(Node));
			if (!prohibitsInitProperties)
			{
				ConstantValue = new Item<object>();
			}
        }
        public static readonly int ConstantValueIndex =  TypeDeclarationIndex + 1; 		
        protected Item<object> GetConstantValue() 
		{ 
			return (Item<object>)Properties[ConstantValueIndex]; 
		}
        protected Item<object> SetConstantValue(Item<object> value) 
		{ 
			value.Name = "ConstantValue";
			Properties[ConstantValueIndex] = value; 
			return value; 
		}
        Item<object> constantValue;
        public Item<object> ConstantValue 
		{ 
			get { return constantValue; } 
			set { constantValue = SetConstantValue(CheckCanModify(value)); } 
		}


    }
}

