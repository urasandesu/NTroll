using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.ILTools;
using Urasandesu.NAnonym.Mixins.System;
using Urasandesu.NAnonym;
using System.Reflection;
using System.Collections.ObjectModel;
using Urasandesu.NAnonym.Mixins.System.Reflection;

namespace Urasandesu.NTroll.FormulaSample2.Formulas
{
    public static class FormulaRef
    {
        public static IFormula Accept(IFormulaRef formula, IFormulaVisitor visitor)
        {
            throw new InvalidOperationException("Must convert this to an object implemented IFormula by calling Establish().");
        }

        public static IAssignFormulaRef Assign(IFormulaRef left, IFormulaRef right)
        {
            return new AssignFormulaRef(left, right, null, null, null);
        }

        public static IVariableFormulaRef Variable(Type type, string name)
        {
            return Variable(type.ToTypeDecl(), name);
        }

        public static IVariableFormulaRef Variable(ITypeDeclaration type, string name)
        {
            return new VariableFormulaRef(type, name, default(IFormulaRef));
        }

        public static IBlockFormulaRef Block(IBlockFormulaRef parentBlock)
        {
            return new BlockFormulaRef() { ParentBlock = parentBlock };
        }

        public static IConstantFormulaRef Constant(object value)
        {
            Required.NotDefault(value, () => value);
            return Constant(value, value.GetType().ToTypeDecl(), null);
        }

        public static IConstantFormulaRef Constant(object value, Type type)
        {
            Required.NotDefault(type, () => type);
            return Constant(value, type.ToTypeDecl(), null);
        }

        public static IConstantFormulaRef Constant(object value, ITypeDeclaration type, IFormulaRef parent)
        {
            return new ConstantFormulaRef(value, type, parent);
        }

        public static IConditionalFormulaRef Condition(IFormulaRef test)
        {
            return Condition(test, null, null);
        }

        public static IConditionalFormulaRef Condition(IFormulaRef test, IFormulaRef ifTrue, IFormulaRef ifFalse)
        {
            return new ConditionalFormulaRef(test, ifTrue, ifFalse, null, null);
        }

        public static IAddFormulaRef Add(IFormulaRef left, IFormulaRef right)
        {
            Required.NotDefault(left, () => left);
            return new AddFormulaRef(left, right, left.Type, null, null);
        }

        public static IMultiplyFormulaRef Multiply(IFormulaRef left, IFormulaRef right)
        {
            Required.NotDefault(left, () => left);
            return new MultiplyFormulaRef(left, right, left.Type, null, null);
        }

        public static IExclusiveOrFormulaRef ExclusiveOr(IFormulaRef left, IFormulaRef right)
        {
            Required.NotDefault(left, () => left);
            return new ExclusiveOrFormulaRef(left, right, left.Type, null, null);
        }

        public static IEqualFormulaRef Equal(IFormulaRef left, IFormulaRef right)
        {
            return Equal(left, right, typeof(bool));
        }

        public static IEqualFormulaRef Equal(IFormulaRef left, IFormulaRef right, Type type)
        {
            Required.NotDefault(type, () => type);
            return new EqualFormulaRef(left, right, type.ToTypeDecl(), null, null);
        }

        public static INotEqualFormulaRef NotEqual(IFormulaRef left, IFormulaRef right)
        {
            return NotEqual(left, right, typeof(bool));
        }

        public static INotEqualFormulaRef NotEqual(IFormulaRef left, IFormulaRef right, Type type)
        {
            Required.NotDefault(type, () => type);
            return new NotEqualFormulaRef(left, right, type.ToTypeDecl(), null, null);
        }

        public static IAndAlsoFormulaRef AndAlso(IFormulaRef left, IFormulaRef right)
        {
            return AndAlso(left, right, typeof(bool));
        }

        public static IAndAlsoFormulaRef AndAlso(IFormulaRef left, IFormulaRef right, Type type)
        {
            Required.NotDefault(type, () => type);
            return new AndAlsoFormulaRef(left, right, type.ToTypeDecl(), null, null);
        }

        public static IMethodCallFormulaRef Call(MethodInfo method, params IFormulaRef[] arguments)
        {
            if (!method.IsStatic) throw new ArgumentException("The parameter must be static method.", TypeSavable.GetName(() => method));
            Required.NotDefault(method, () => method);
            Required.NotDefault(arguments, () => arguments);
            return Call(default(IFormulaRef), method.ToMethodDecl(), new Collection<IFormulaRef>(arguments), method.ReturnType.ToTypeDecl(), null);
        }

        public static IMethodCallFormulaRef Call(IFormulaRef instance, MethodInfo method, params IFormulaRef[] arguments)
        {
            if (instance == null)
            {
                return Call(method, arguments);
            }
            else
            {
                Required.NotDefault(method, () => method);
                Required.NotDefault(arguments, () => arguments);
                return Call(instance, method.ToMethodDecl(), new Collection<IFormulaRef>(arguments), method.ReturnType.ToTypeDecl(), null);
            }
        }

        public static IMethodCallFormulaRef Call(IFormulaRef instance, IMethodDeclaration method, Collection<IFormulaRef> arguments, ITypeDeclaration type, IFormulaRef parent)
        {
            return new MethodCallFormulaRef(instance, method, arguments, type, parent);
        }

        public static IConvertFormulaRef Convert(IFormulaRef operand, Type type)
        {
            Required.NotDefault(type, () => type);
            return Convert(type.ToTypeDecl(), operand, null, null);
        }

        public static IConvertFormulaRef Convert(ITypeDeclaration type, IFormulaRef operand, IMethodDeclaration method, IFormulaRef parent)
        {
            return new ConvertFormulaRef(type, operand, method, parent);
        }

        public static ITypeAsFormulaRef TypeAs(IFormulaRef operand, Type type)
        {
            Required.NotDefault(type, () => type);
            return TypeAs(type.ToTypeDecl(), operand, null, null);
        }

        public static ITypeAsFormulaRef TypeAs(ITypeDeclaration type, IFormulaRef operand, IMethodDeclaration method, IFormulaRef parent)
        {
            return new TypeAsFormulaRef(type, operand, method, parent);
        }

        public static IReturnFormulaRef Return(IFormulaRef @object)
        {
            Required.NotDefault(@object, () => @object);
            return new ReturnFormulaRef(@object, @object.Type, null);
        }
    }
}
