using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class ExpressionToFormulaState
    {
        public ExpressionToFormulaState()
        {
            CurrentBlock = null;
            PushBlock();
            Arguments = new Collection<Formula>();
            Conditions = new Collection<ConditionalFormula>();
        }

        public void PushBlock()
        {
            CurrentBlock = new BlockFormula() { ParentBlock = CurrentBlock };
        }

        public void PopBlock()
        {
            if (CurrentBlock == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                CurrentBlock = CurrentBlock.ParentBlock;
            }
        }

        public BlockFormula CurrentBlock { get; private set; }
        public Collection<Formula> Arguments { get; private set; }
        public Collection<ConditionalFormula> Conditions { get; private set; }
    }
}
