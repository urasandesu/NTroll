using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym;
using Urasandesu.NAnonym.Linq;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public class MethodCallFormulaRef : MethodCallFormula, IMethodCallFormulaRef
    {
        protected internal MethodCallFormulaRef()
            : this(
                default(IFormulaRef), 
                default(IMethodDeclaration), 
                new Collection<IFormulaRef>(), 
                default(ITypeDeclaration), 
                default(IFormulaRef))
        {
        }

        protected internal MethodCallFormulaRef(
            IFormulaRef instance,
            IMethodDeclaration method,
            Collection<IFormulaRef> arguments,
            ITypeDeclaration type,
            IFormulaRef parent)
            : base(
                instance, 
                method, 
                arguments.ToReadOnlyWithCast<IFormula>(), 
                type, 
                parent)
        {
            this.parent = parent;
            this.instance = instance;
            this.arguments = arguments;
        }

        public new NodeType NodeType { get { return base.NodeType; } set { base.NodeType = value; } }
        public new ITypeDeclaration Type { get { return base.Type; } set { base.Type = value; } }
        IFormulaRef parent;
        public new IFormulaRef Parent { get { return parent; } set { parent = value; base.Parent = parent; } }

        IFormulaRef instance;
        public new IFormulaRef Instance { get { return instance; } set { instance = value; base.Instance = instance; } }
        public new IMethodDeclaration Method { get { return base.Method; } set { base.Method = value; } }
        Collection<IFormulaRef> arguments;
        public new Collection<IFormulaRef> Arguments { get { return arguments; } set { arguments = value; base.Arguments = arguments.ToReadOnlyWithCast<IFormula>(); } }

        public override IFormula Accept(IFormulaVisitor visitor)
        {
            return FormulaRef.Accept(this, visitor);
        }

        public IMethodCallFormula Establish()
        {
            var established = default(IMethodCallFormula);
            FirstHierarchyOnly(() =>
            {
                var instance = Instance == null ? default(IFormula) : Instance.Establish();
                var method = Method;
                var arguments = Arguments == null ? default(ReadOnlyCollection<IFormula>) : Arguments.Select(_ => _.Establish()).ToReadOnly();
                var type = Type;
                var parent = Parent == null ? default(IFormula) : Parent.Establish();
                established = Formula.Call(instance, method, arguments, type, parent);
            });
            return established;
        }

        IFormula IFormulaRef.Establish()
        {
            return Establish();
        }
    }
}
