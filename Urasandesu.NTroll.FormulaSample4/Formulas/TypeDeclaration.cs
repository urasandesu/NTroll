using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class TypeDeclaration : Terminal<ITypeDeclaration>
    {
        public TypeDeclaration(ITypeDeclaration type)
            : base(INonterminalMixin.TypeDeclarationName, type, default(INode))
        {
        }
    }
}
