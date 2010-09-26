using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Cecil.DI;
using Urasandesu.NTroll.GlobalClassTestSample;

namespace Test.Urasandesu.NTroll.GlobalClassTestSample.DI
{
    public class GlobalClass1_01 : GlobalClassBase
    {
        protected override GlobalClassBase OnSetup()
        {
            var class1 = new GlobalClass<Class1>();
            class1.Setup(the =>
            {
                the.Method(
                (string value) =>
                {
                    return value + value + value;
                }).
                Instead(_ => _.DoNothing);

                the.Method(
                (string value) =>
                {
                    return value;
                }).
                Instead(_ => _.Return3TimesIfValueContainsA);
            });
            return class1;
        }

        protected override string AssemblyCodeBase
        {
            get { return typeof(Class1).Assembly.CodeBase; }
        }

        protected override string AssemblyLocation
        {
            get { return typeof(Class1).Assembly.Location; }
        }
    }
}
