using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;

namespace Urasandesu.NTroll.DomainFree
{
    public sealed class LooseDomain<T> where T : class
    {
        static readonly object ms_lockObj = new object();
        static T ms_instance = null;
        static bool ms_ready = false;

        LooseDomain() { }

        public static void Register(Func<T> instanceGetter)
        {
            if (!instanceGetter.Method.IsStatic)
                throw new ArgumentException("The parameter must be the reference of a " + 
                                            "static method.", "instanceGetter");

            var funcPtr = instanceGetter.Method.MethodHandle.GetFunctionPointer();
            CrossDomainDictionary<Type, IntPtr>.Instance.AddIfNotExist(typeof(T), funcPtr);
        }

        static T GetInstance()
        {
            var funcPtr = CrossDomainDictionary<Type, IntPtr>.Instance.GetIfExist(typeof(T));
            if (funcPtr == default(IntPtr))
                throw new InvalidOperationException("The instance getter of T has not been " + 
                            "registered yet. Please call the method Register and give a " + 
                            "instance getter to it.");

            var instanceGetter = new DynamicMethod("Instantiator", typeof(T), null, typeof(T).Module);
            var gen = instanceGetter.GetILGenerator();
            if (IntPtr.Size == 4)
            {
                gen.Emit(OpCodes.Ldc_I4, funcPtr.ToInt32());
            }
            else if (IntPtr.Size == 8)
            {
                gen.Emit(OpCodes.Ldc_I8, funcPtr.ToInt64());
            }
            else
            {
                throw new NotSupportedException();
            }
            gen.EmitCalli(OpCodes.Calli, CallingConventions.Standard, typeof(T), null, null);
            gen.Emit(OpCodes.Ret);
            return ((Func<T>)instanceGetter.CreateDelegate(typeof(Func<T>)))();
        }

        public static T Instance
        {
            get
            {
                if (!ms_ready)
                {
                    lock (ms_lockObj)
                    {
                        if (!ms_ready)
                        {
                            ms_instance = GetInstance();
                            Thread.MemoryBarrier();
                            ms_ready = true;
                        }
                    }
                }
                // !! Type is not checked by CLR if a opcode castclass does not exist !!
                return ms_instance; 
            }
        }

        public static void Unload()
        {
            if (ms_ready)
            {
                lock (ms_lockObj)
                {
                    if (ms_ready)
                    {
                        CrossDomainDictionary<Type, IntPtr>.Unload();
                        ms_instance = null;
                        Thread.MemoryBarrier();
                        ms_ready = false;
                    }
                }
            }
        }
    }
}
