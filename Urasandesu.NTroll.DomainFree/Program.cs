using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.DomainFree
{
    public class MyCounter
    {
        int m_value;
        public void Increment() { ++m_value; }
        public int Value { get { return m_value; } }
    }

    public class MySingleton
    {
        MySingleton()
        {
            Counter = new MyCounter();
        }
        static MySingleton m_instance = new MySingleton();
        public static MySingleton Instance { get { return m_instance; } }
        public MyCounter Counter { get; private set; }
    }

    public class MarshalByRefRunner : MarshalByRefObject
    {
        public Action Action { get; set; }
        public void Run()
        {
            if (Action != null)
            {
                Action();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("MySingleton.Instance: {0}", MySingleton.Instance.GetHashCode());
            MySingleton.Instance.Counter.Increment();
            Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", MySingleton.Instance.Counter.Value);

            DomainFreeInstantiator.Register(() => MySingleton.Instance);
            Console.WriteLine("MySingleton.Instance: {0}", DomainFreeInstantiator.Get<MySingleton>().GetHashCode());
            DomainFreeInstantiator.Get<MySingleton>().Counter.Increment();
            Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", DomainFreeInstantiator.Get<MySingleton>().Counter.Value);

            var newDomain = AppDomain.CreateDomain("New Domain");
            var runner = (MarshalByRefRunner)newDomain.CreateInstanceAndUnwrap(typeof(MarshalByRefRunner).Assembly.FullName, typeof(MarshalByRefRunner).FullName);
            runner.Action = () =>
            {
                Console.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine("MySingleton.Instance: {0}", DomainFreeInstantiator.Get<MySingleton>().GetHashCode());
                DomainFreeInstantiator.Get<MySingleton>().Counter.Increment();
                Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", DomainFreeInstantiator.Get<MySingleton>().Counter.Value);
            };
            runner.Run();
        }
    }
}
