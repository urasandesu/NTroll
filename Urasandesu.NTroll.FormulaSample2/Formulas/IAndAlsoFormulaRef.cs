﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public interface IAndAlsoFormulaRef : IAndAlsoFormula, IBinaryFormulaRef
    {
        new IAndAlsoFormula Establish();
    }
}
