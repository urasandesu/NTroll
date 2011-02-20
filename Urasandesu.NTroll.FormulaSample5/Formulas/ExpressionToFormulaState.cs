using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    class ExpressionToFormulaState
    {
        public ExpressionToFormulaState()
        {
            CurrentBlock = new NullBlockFormula();
            PushBlock();
            Arguments = new Collection<Formula>();
        }

        public void PushBlock()
        {
            CurrentBlock = new BlockFormula() { ParentBlock = CurrentBlock };
        }

        public void PopBlock()
        {
            if (CurrentBlock is NullBlockFormula)
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
    }
}
