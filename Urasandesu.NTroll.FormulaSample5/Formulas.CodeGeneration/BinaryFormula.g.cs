using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class BinaryFormula : Formula
    {
        public BinaryFormula()
            : this(default(bool))
        {
        }

        public BinaryFormula(bool prohibitsInitProperties)
            : base()
        {
			NodeType.Value = FormulaType.None;
            Properties.Insert(LeftIndex, default(Node));
            Properties.Insert(MethodIndex, default(Node));
            Properties.Insert(RightIndex, default(Node));
			if (!prohibitsInitProperties)
			{
				Left = new NullFormula();
				Method = new Item<IMethodDeclaration>();
				Right = new NullFormula();
			}
        }
        public static readonly int LeftIndex =  TypeDeclarationIndex + 1; 		
        protected Formula GetLeft() 
		{ 
			return (Formula)Properties[LeftIndex]; 
		}
        protected Formula SetLeft(Formula value) 
		{ 
			value.Name = "Left";
			value.Referrer = this; 
			Properties[LeftIndex] = value; 
			return value; 
		}
        Formula left;
        public Formula Left 
		{ 
			get { return left; } 
			set { left = SetLeft(CheckCanModify(value)); } 
		}


        public static readonly int MethodIndex =  LeftIndex + 1; 		
        protected Item<IMethodDeclaration> GetMethod() 
		{ 
			return (Item<IMethodDeclaration>)Properties[MethodIndex]; 
		}
        protected Item<IMethodDeclaration> SetMethod(Item<IMethodDeclaration> value) 
		{ 
			value.Name = "Method";
			Properties[MethodIndex] = value; 
			return value; 
		}
        Item<IMethodDeclaration> method;
        public Item<IMethodDeclaration> Method 
		{ 
			get { return method; } 
			set { method = SetMethod(CheckCanModify(value)); } 
		}


        public static readonly int RightIndex =  MethodIndex + 1; 		
        protected Formula GetRight() 
		{ 
			return (Formula)Properties[RightIndex]; 
		}
        protected Formula SetRight(Formula value) 
		{ 
			value.Name = "Right";
			value.Referrer = this; 
			Properties[RightIndex] = value; 
			return value; 
		}
        Formula right;
        public Formula Right 
		{ 
			get { return right; } 
			set { right = SetRight(CheckCanModify(value)); } 
		}


    }
}

