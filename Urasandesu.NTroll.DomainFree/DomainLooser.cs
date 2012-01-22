using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Urasandesu.NTroll.DomainFree
{
    public class DomainLooser<T>
    {
        public static void RegisterInstantiator(Func<T> instantiator)
        {
            if (!instantiator.Method.IsStatic)
                throw new ArgumentException("The parameter must be reference of static method.", "instantiator");
            CrossDomainDictionary<Type, IntPtr>.Instance.AddIfNotExist(typeof(T), instantiator.Method.MethodHandle.GetFunctionPointer());
        }

        static Dictionary<Type, Func<T>> m_instantiators = new Dictionary<Type, Func<T>>();
        public static T Instance
        {
            get
            {
                if (!m_instantiators.ContainsKey(typeof(T)))
                {
                    var funcPtr = CrossDomainDictionary<Type, IntPtr>.Instance.GetIfExist(typeof(T));
                    var instantiator = new DynamicMethod("Instantiator", typeof(T), null, typeof(T).Module);
                    var gen = instantiator.GetILGenerator();
                    gen.Emit(OpCodes.Ldc_I4, funcPtr.ToInt32());
                    gen.EmitCalli(OpCodes.Calli, CallingConventions.Standard, typeof(T), null, null);
                    gen.Emit(OpCodes.Ret);
                    m_instantiators.Add(typeof(T), (Func<T>)instantiator.CreateDelegate(typeof(Func<T>)));
                }
                return m_instantiators[typeof(T)](); // !! Type is not checked by CLR if a opcode castclass does not exist !!
            }
        }
    }
}
