using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public class Hierarchy
    {
        public void DoIfFirstLevel(Action ifTrue)
        {
            DoIfFirstLevel(ifTrue, () => { });
        }

        public void DoIfFirstLevel(Action ifTrue, Action ifFalse)
        {
            try
            {
                if (UpLevel() == 1)
                {
                    ifTrue();
                }
                else
                {
                    ifFalse();
                }
            }
            finally
            {
                DownLevel();
            }
        }

        public int UpLevel()
        {
            return ++Level;
        }

        public int DownLevel()
        {
            return --Level;
        }

        public int Level { get; private set; }
    }
}
