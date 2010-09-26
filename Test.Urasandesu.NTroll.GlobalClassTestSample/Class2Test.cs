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
    public class Class2Test : NewDomainTestBase
    {
        public Class2Test()
        {
            DependencyUtil.BeginEdit();
            DependencyUtil.Setup<GlobalClass1_02>();
            DependencyUtil.Setup<GlobalClass2_01>();
            DependencyUtil.Load();
        }

        [NewDomainTest]
        public void Test1()
        {
            Assert.AreEqual("abc", new Class1().DoNothing("abc"));
            Assert.AreEqual("abcabcabc", new Class1().Return3TimesIfValueContainsA("abc"));
        }

        [NewDomainTest]
        public void Test2()
        {
            Assert.AreEqual("abc", new Class2().ReturnTrimedStringIfContainsB("            abc       "));
            Assert.AreEqual("cba", new Class2().ReturnReversedStringIfContainsC("abc"));
        }
    }
}
