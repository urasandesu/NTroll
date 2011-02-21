using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public abstract partial class Formula : Node
    {
        public Formula()
            : this(default(bool))
        {
        }

        public Formula(bool prohibitsInitProperties)
            : base()
        {
            Properties.Insert(ReferrerIndex, default(Node));
            Properties.Insert(NodeTypeIndex, default(Node));
            Properties.Insert(TypeDeclarationIndex, default(Node));
			if (!prohibitsInitProperties)
			{
				Referrer = new NullFormula();
				NodeType = new Item<FormulaType>();
				TypeDeclaration = new Item<ITypeDeclaration>();
			}
        }
        public static readonly int ReferrerIndex = 0; 		
        protected Formula GetReferrer() 
		{ 
			return (Formula)Properties[ReferrerIndex]; 
		}
        protected Formula SetReferrer(Formula value) 
		{ 
			Properties[ReferrerIndex] = value; 
			return value; 
		}
        Formula referrer;
        public Formula Referrer 
		{ 
			get { return referrer; } 
			set { referrer = SetReferrer(CheckCanModify(value)); } 
		}


        public static readonly int NodeTypeIndex =  ReferrerIndex + 1; 		
        protected Item<FormulaType> GetNodeType() 
		{ 
			return (Item<FormulaType>)Properties[NodeTypeIndex]; 
		}
        protected Item<FormulaType> SetNodeType(Item<FormulaType> value) 
		{ 
			value.Name = "NodeType";
			Properties[NodeTypeIndex] = value; 
			return value; 
		}
        Item<FormulaType> nodeType;
        public Item<FormulaType> NodeType 
		{ 
			get { return nodeType; } 
			set { nodeType = SetNodeType(CheckCanModify(value)); } 
		}


        public static readonly int TypeDeclarationIndex =  NodeTypeIndex + 1; 		
        protected Item<ITypeDeclaration> GetTypeDeclaration() 
		{ 
			return (Item<ITypeDeclaration>)Properties[TypeDeclarationIndex]; 
		}
        protected Item<ITypeDeclaration> SetTypeDeclaration(Item<ITypeDeclaration> value) 
		{ 
			value.Name = "TypeDeclaration";
			Properties[TypeDeclarationIndex] = value; 
			return value; 
		}
        Item<ITypeDeclaration> typeDeclaration;
        public Item<ITypeDeclaration> TypeDeclaration 
		{ 
			get { return typeDeclaration; } 
			set { typeDeclaration = SetTypeDeclaration(CheckCanModify(value)); } 
		}


    }
}

