using System;
using NUnit.Framework;
using Urasandesu.NTroll.DomainFree;

namespace Test.Urasandesu.NTroll.DomainFree
{
    [TestFixture]
    public class MyTestFixture
    {
        [TestFixtureSetUp]
        public void MyTestFixtureSetUp()
        {
            MyConsole.Unload();
            LooseDomain<MyStopwatch>.Unload();
            LooseDomain<MyStopwatch>.Register(() => MyStopwatch.Instance);
        }

        [TestFixtureTearDown]
        public void MyTestFixtureTearDown()
        {
            MyConsole.Unload();
            LooseDomain<MyStopwatch>.Unload();
        }

        [Test]
        public void MyTest()
        {
            MyConsole.Out.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);

            LooseDomain<MyStopwatch>.Instance.Restart();
            MyConsole.Out.WriteLine("Elapsed: {0} ms", LooseDomain<MyStopwatch>.Instance.ElapsedMilliseconds);
            AppDomain.CurrentDomain.RunAtIsolatedDomain(() =>
            {
                MyConsole.Out.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
                MyConsole.Out.WriteLine("Elapsed: {0} ms", LooseDomain<MyStopwatch>.Instance.ElapsedMilliseconds);

                LooseDomain<MyStopwatch>.Instance.Restart();
            });
            MyConsole.Out.WriteLine("AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
            MyConsole.Out.WriteLine("Elapsed: {0} ms", LooseDomain<MyStopwatch>.Instance.ElapsedMilliseconds);

            LooseDomain<MyStopwatch>.Instance.Restart();
            MyConsole.Out.WriteLine("Elapsed: {0} ms", LooseDomain<MyStopwatch>.Instance.ElapsedMilliseconds);

            // ***** Test.Urasandesu.NTroll.DomainFree.MyTestFixture.MyTest
            // AppDomain: test-domain-Test.Urasandesu.NTroll.DomainFree.nunit
            // Elapsed: 0 ms
            // AppDomain: Domain Void <MyTest>b__2()
            // Elapsed: 78 ms
            // AppDomain: test-domain-Test.Urasandesu.NTroll.DomainFree.nunit
            // Elapsed: 1 ms
            // Elapsed: 0 ms
        }
    }
}
