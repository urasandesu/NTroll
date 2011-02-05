using System;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.ILTools;
using System.Text;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class BlockFormula : Formula, IBlockFormula
    {
        protected internal BlockFormula(
            IBlockFormula parentBlock, 
            ReadOnlyCollection<IBlockFormula> childBlocks,
            ReadOnlyCollection<IFormula> variables, 
            ReadOnlyCollection<IFormula> formulas, 
            IFormula result, 
            ITypeDeclaration type, 
            IFormula parent)
            : base(NodeType.Block, type, parent)
        {
            ParentBlock = parentBlock;
            ChildBlocks = childBlocks;
            Variables = variables;
            Formulas = formulas;
            Result = result;
        }

        public IBlockFormula ParentBlock { get; protected set; }
        public ReadOnlyCollection<IBlockFormula> ChildBlocks { get; protected set; }
        public ReadOnlyCollection<IFormula> Variables { get; protected set; }
        public ReadOnlyCollection<IFormula> Formulas { get; protected set; }
        public IFormula Result { get; protected set; }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            throw new NotImplementedException();
        }

        protected override void AppendToString(StringBuilder sb)
        {
            base.AppendToString(sb);
            sb.Append(", ");
            sb.Append("\"ParentBlock\": ");
            sb.Append(ParentBlock.NullableToString());
            sb.Append(", ");
            sb.Append("\"ChildBlocks\": ");
            sb.Append(ChildBlocks.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
            sb.Append(", ");
            sb.Append("\"Variables\": ");
            sb.Append(Variables.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
            sb.Append(", ");
            sb.Append("\"Formulas\": ");
            sb.Append(Formulas.NullableJoinToString("[", ", ", _ => _.NullableToString(), "]"));
            sb.Append(", ");
            sb.Append("\"Result\": ");
            sb.Append(Result.NullableToString());
        }
    }
}
