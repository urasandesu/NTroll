using System;
using System.Runtime.CompilerServices;

namespace Urasandesu.NTroll.DomainFree
{
    public class MClass : MarshalByRefObject
    {
        public object InstanceMember { get; set; }
        public static object StaticMember { get; set; }
        public void Run()
        {
            Console.WriteLine("AppDomain: {0}",
                              AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("MarshalByRefObject Class Static Member: {0}",
                              RuntimeHelpers.GetHashCode(StaticMember));
            Console.WriteLine("MarshalByRefObject Class Instance Member: {0}",
                              RuntimeHelpers.GetHashCode(InstanceMember));
        }
    }
}
