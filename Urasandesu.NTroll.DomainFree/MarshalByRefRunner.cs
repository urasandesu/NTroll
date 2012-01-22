using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.DomainFree
{
    public class MarshalByRefRunner : MarshalByRefObject
    {
        public Action Action { get; set; }
        public void Run()
        {
            if (Action != null)
                Action();
        }
    }
}
