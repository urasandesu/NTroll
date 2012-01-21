using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Urasandesu.NTroll.DomainFree
{
    public class DomainFreeInstantiator
    {
        public static void Register<T>(Func<T> instantiator)
        {
            DomainFreeDictionary<Type, IntPtr>.Instance.AddIfNotExist(typeof(T), instantiator.Method.MethodHandle.GetFunctionPointer());
        }

        static Dictionary<Type, Delegate> m_instantiators = new Dictionary<Type, Delegate>();
        public static T Get<T>()
        {
            if (!m_instantiators.ContainsKey(typeof(T)))
            {
                var funcPtr = DomainFreeDictionary<Type, IntPtr>.Instance.GetIfExist(typeof(T));
                var instantiator = new DynamicMethod("Instantiator", typeof(T), null, typeof(T).Module);
                var gen = instantiator.GetILGenerator();
                gen.Emit(OpCodes.Ldc_I4, funcPtr.ToInt32());
                gen.EmitCalli(OpCodes.Calli, CallingConventions.Standard, typeof(T), null, null);
                gen.Emit(OpCodes.Ret);
                m_instantiators.Add(typeof(T), instantiator.CreateDelegate(typeof(Func<T>)));
            }
            return ((Func<T>)m_instantiators[typeof(T)])(); // !! Type is not checked by CLR if a opcode castclass does not exist !!
        }
    }
}
