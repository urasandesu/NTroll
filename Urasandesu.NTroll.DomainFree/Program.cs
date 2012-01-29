//#define MCLASS_SAMPLE
#define SCLASS_SAMPLE

#if _
#elif MCLASS_SAMPLE
using System;
using System.Runtime.Remoting;

namespace Urasandesu.NTroll.DomainFree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MarshalByRefObject Class Test: " +
                              "Default AppDomain -> Test Domain");
            {
                MClass.StaticMember = new object();
                var o = new MClass();
                Console.WriteLine("o is Transparent Proxy?: {0}",
                                  RemotingServices.IsTransparentProxy(o));
                o.InstanceMember = new object();
                o.Run();

                var info = AppDomain.CurrentDomain.SetupInformation;
                var testDomain = AppDomain.CreateDomain("Test Domain", null, info);
                testDomain.DoCallBack(new CrossAppDomainDelegate(o.Run));
                AppDomain.Unload(testDomain);
            }
            Console.WriteLine();


            Console.WriteLine("MarshalByRefObject Class Test: " +
                              "Default AppDomain <- Test Domain");
            {
                var info = AppDomain.CurrentDomain.SetupInformation;
                var testDomain = AppDomain.CreateDomain("Test Domain", null, info);
                var t = typeof(MClass);
                var o = (MClass)testDomain.CreateInstanceAndUnwrap(t.Assembly.FullName, t.FullName);
                Console.WriteLine("o is Transparent Proxy?: {0}",
                                  RemotingServices.IsTransparentProxy(o));

                MClass.StaticMember = new object();
                o.InstanceMember = new object();
                o.Run();

                testDomain.DoCallBack(new CrossAppDomainDelegate(o.Run));
                AppDomain.Unload(testDomain);
            }
            Console.WriteLine();

            // The example displays the following output:
            // 
            //    MarshalByRefObject Class Test: Default AppDomain -> Test Domain
            //    o is Transparent Proxy?: False
            //    AppDomain: Urasandesu.NTroll.DomainFree.exe
            //    MarshalByRefObject Class Static Member: 54267293
            //    MarshalByRefObject Class Instance Member: 18643596
            //    AppDomain: Urasandesu.NTroll.DomainFree.exe
            //    MarshalByRefObject Class Static Member: 54267293
            //    MarshalByRefObject Class Instance Member: 18643596
            //    
            //    MarshalByRefObject Class Test: Default AppDomain <- Test Domain
            //    o is Transparent Proxy?: True
            //    AppDomain: Test Domain
            //    MarshalByRefObject Class Static Member: 0
            //    MarshalByRefObject Class Instance Member: 55915408
            //    AppDomain: Test Domain
            //    MarshalByRefObject Class Static Member: 0
            //    MarshalByRefObject Class Instance Member: 55915408
        }
    }
}
#elif SCLASS_SAMPLE
using System;
using System.Runtime.Remoting;

namespace Urasandesu.NTroll.DomainFree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Serializable Class Test: " + 
                              "Default AppDomain -> Test Domain"); 
            {
                SClass.StaticMember = new object();
                var o = new SClass();
                Console.WriteLine("o is Transparent Proxy?: {0}", 
                                  RemotingServices.IsTransparentProxy(o));
                o.InstanceMember = new object();
                o.Run();

                var info = AppDomain.CurrentDomain.SetupInformation;
                var testDomain = AppDomain.CreateDomain("Test Domain", null, info);
                testDomain.DoCallBack(new CrossAppDomainDelegate(o.Run));
                AppDomain.Unload(testDomain);
            }
            Console.WriteLine();


            Console.WriteLine("Serializable Class Test: " + 
                              "Default AppDomain <- Test Domain");
            {
                var info = AppDomain.CurrentDomain.SetupInformation;
                var testDomain = AppDomain.CreateDomain("Test Domain", null, info);
                var t = typeof(SClass);
                var o = (SClass)testDomain.CreateInstanceAndUnwrap(t.Assembly.FullName, t.FullName);
                Console.WriteLine("o is Transparent Proxy?: {0}",
                                  RemotingServices.IsTransparentProxy(o));

                SClass.StaticMember = new object();
                o.InstanceMember = new object();
                o.Run();

                testDomain.DoCallBack(new CrossAppDomainDelegate(o.Run));
                AppDomain.Unload(testDomain);
            }
            Console.WriteLine();

            // The example displays the following output:
            // 
            //    Serializable Class Test: Default AppDomain -> Test Domain
            //    o is Transparent Proxy?: False
            //    AppDomain: Urasandesu.NTroll.DomainFree.exe
            //    Serializable Class Static Member: 54267293
            //    Serializable Class Instance Member: 18643596
            //    AppDomain: Test Domain
            //    Serializable Class Static Member: 0
            //    Serializable Class Instance Member: 18796293
            //    
            //    Serializable Class Test: Default AppDomain <- Test Domain
            //    o is Transparent Proxy?: False
            //    AppDomain: Urasandesu.NTroll.DomainFree.exe
            //    Serializable Class Static Member: 12289376
            //    Serializable Class Instance Member: 43495525
            //    AppDomain: Test Domain
            //    Serializable Class Static Member: 0
            //    Serializable Class Instance Member: 43942917
        }
    }
}
#endif