﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Urasandesu.NTroll.FormulaSample5.Formulas
{
    public partial class BlockFormula : Formula
    {
        protected override void Initialize()
        {
            base.Initialize();
            PropertyChanged += new PropertyChangedEventHandler(BlockFormula_PropertyChanged);
        }

        void BlockFormula_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == FormulaCollection<Formula>.NameOfCount)
            {
                Result = Formulas == null || Formulas.Count == 0 ? null : Formulas[Formulas.Count - 1];
            }
            else if (e.PropertyName == BlockFormula.NameOfResult ||
                     sender != this && e.PropertyName == BlockFormula.NameOfTypeDeclaration)
            {
                TypeDeclaration = Result == null ? null : Result.TypeDeclaration;
            }
        }
    }
}
