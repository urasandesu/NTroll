using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;

namespace Urasandesu.NTroll.FormulaSample4.Formulas
{
    public class TypeDeclarationRef : TerminalRef<ITypeDeclaration>
    {
        public TypeDeclarationRef(ITypeDeclaration type)
            : base(INonterminalMixin.TypeDeclarationName, type, default(INodeRef))
        {
        }

        protected override ITerminal<ITypeDeclaration> Pin(string name, ITypeDeclaration value, INode referrers)
        {
            return new TypeDeclaration(value);
        }
    }
}
