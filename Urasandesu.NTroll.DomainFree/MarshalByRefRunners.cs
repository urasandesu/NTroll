using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.DomainFree
{
    public class MarshalByRefAction : MarshalByRefObject
    {
        public Action Action { get; set; }
        public void Run()
        {
            if (Action != null)
                Action();
        }
    }

    public class MarshalByRefAction<T> : MarshalByRefObject
    {
        public Action<T> Action { get; set; }
        public void Run(T obj)
        {
            if (Action != null)
                Action(obj);
        }
    }
}
