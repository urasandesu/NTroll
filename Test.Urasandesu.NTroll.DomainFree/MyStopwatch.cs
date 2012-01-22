using System.Diagnostics;

namespace Test.Urasandesu.NTroll.DomainFree
{
    public class MyStopwatch : Stopwatch
    {
        MyStopwatch() { }
        static MyStopwatch ms_instance = new MyStopwatch();
        public static MyStopwatch Instance { get { return ms_instance; } }
        public void Restart()
        {
            Reset();
            Start();
        }
    }
}
