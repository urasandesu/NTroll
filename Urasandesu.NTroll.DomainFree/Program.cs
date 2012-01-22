using System;

namespace Urasandesu.NTroll.DomainFree
{
    public class MyFunc<TResult>
    {
        MyFunc() { }
        static MyFunc<TResult> m_instance = new MyFunc<TResult>();
        public static MyFunc<TResult> Instance { get { return m_instance; } }
        public Func<TResult> Func { get; set; }
    }

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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("MySingleton.Instance: {0}", MySingleton.Instance.GetHashCode());
            MySingleton.Instance.Counter.Increment();
            Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", MySingleton.Instance.Counter.Value);

            LooseDomain<MySingleton>.Register(() => MySingleton.Instance);
            Console.WriteLine("MySingleton.Instance: {0}", LooseDomain<MySingleton>.Instance.GetHashCode());
            LooseDomain<MySingleton>.Instance.Counter.Increment();
            Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", LooseDomain<MySingleton>.Instance.Counter.Value);

            LooseDomain<MyFunc<MyCounter>>.Register(() => MyFunc<MyCounter>.Instance);
            {
                var counter = new MyCounter();
                counter.Increment();
                LooseDomain<MyFunc<MyCounter>>.Instance.Func = () => counter;
                Console.WriteLine("************** Counter: {0}", LooseDomain<MyFunc<MyCounter>>.Instance.Func().Value);
            }

            var newDomain = AppDomain.CreateDomain("New Domain");
            var runner = (MarshalByRefRunner)newDomain.CreateInstanceAndUnwrap(typeof(MarshalByRefRunner).Assembly.FullName, typeof(MarshalByRefRunner).FullName);
            runner.Action = () =>
            {
                Console.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
                Console.WriteLine("MySingleton.Instance: {0}", LooseDomain<MySingleton>.Instance.GetHashCode());
                LooseDomain<MySingleton>.Instance.Counter.Increment();
                Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", LooseDomain<MySingleton>.Instance.Counter.Value);

                LooseDomain<MyFunc<MyCounter>>.Instance.Func().Increment();
                Console.WriteLine("************** Counter: {0}", LooseDomain<MyFunc<MyCounter>>.Instance.Func().Value);

                var counter = new MyCounter();
                counter.Increment();
                LooseDomain<MyFunc<MyCounter>>.Instance.Func = () => counter;
                Console.WriteLine("************** Counter: {0}", LooseDomain<MyFunc<MyCounter>>.Instance.Func().Value);
            };
            runner.Run();

            Console.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("MySingleton.Instance: {0}", LooseDomain<MySingleton>.Instance.GetHashCode());
            LooseDomain<MySingleton>.Instance.Counter.Increment();
            Console.WriteLine("MySingleton.Instance.Counter.Value: {0}", LooseDomain<MySingleton>.Instance.Counter.Value);
            LooseDomain<MyFunc<MyCounter>>.Instance.Func().Increment();
            Console.WriteLine("************** Counter: {0}", LooseDomain<MyFunc<MyCounter>>.Instance.Func().Value);
        }
    }
}
