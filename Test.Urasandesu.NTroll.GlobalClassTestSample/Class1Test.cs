using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Urasandesu.NAnonym.Test;
using Assert = Urasandesu.NAnonym.Test.Assert;
using Urasandesu.NTroll.GlobalClassTestSample;
using Urasandesu.NAnonym.Cecil.DI;
using Test.Urasandesu.NTroll.GlobalClassTestSample.DI;

namespace Test.Urasandesu.NTroll.GlobalClassTestSample
{
    [NewDomainTestFixture]
    public class Class1Test : NewDomainTestBase
    {
        public Class1Test()
        {
            DependencyUtil.Setup<GlobalClass1_01>();
            DependencyUtil.Load();
        }

        [NewDomainTest]
        public void Test1()
        {
            Assert.AreEqual("abcabcabc", new Class1().DoNothing("abc"));
            Assert.AreEqual("abc", new Class1().Return3TimesIfValueContainsA("abc"));
        }
    }
}
