﻿using System;
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

        public static void Register(Func<T> instantiator)
        {
            if (!instantiator.Method.IsStatic)
                throw new ArgumentException("The parameter must be the reference of a " + 
                                            "static method.", "instantiator");

            var funcPtr = instantiator.Method.MethodHandle.GetFunctionPointer();
            CrossDomainDictionary<Type, IntPtr>.Instance.AddIfNotExist(typeof(T), funcPtr);
        }

        static T CreateInstance()
        {
            var funcPtr = CrossDomainDictionary<Type, IntPtr>.Instance.GetIfExist(typeof(T));
            if (funcPtr == default(IntPtr))
                throw new InvalidOperationException("The instantiator of T has not been " + 
                            "registered yet. Please call the method Register and give a " + 
                            "instantiator to it.");

            var instantiator = new DynamicMethod("Instantiator", typeof(T), null, typeof(T).Module);
            var gen = instantiator.GetILGenerator();
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
            return ((Func<T>)instantiator.CreateDelegate(typeof(Func<T>)))();
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
                            ms_instance = CreateInstance();
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
