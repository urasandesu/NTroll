using System;
using System.IO;
using Urasandesu.NTroll.DomainFree;

namespace Test.Urasandesu.NTroll.DomainFree
{
    class MyConsole : CrossDomainDictionary<Type, MyConsole>
    {
        MyConsole() { }
        static MyConsole ms_instance_ = null;
        static MyConsole Instance_
        {
            get
            {
                if (ms_instance_ == null)
                {
                    if (!Instance.ContainsKey(typeof(MyConsole)))
                        Instance.Add(typeof(MyConsole), new MyConsole());
                    
                    ms_instance_ = Instance.Get(typeof(MyConsole));
                }
                return ms_instance_;
            }
        }
        TextWriter m_out = Console.Out;
        TextWriter Out_() { return m_out; }
        public static TextWriter Out { get { return Instance_.Out_(); } }
        public new static void Unload()
        {
            ms_instance_ = null;
            CrossDomainDictionary<Type, MyConsole>.Unload();
        }
    }
}
