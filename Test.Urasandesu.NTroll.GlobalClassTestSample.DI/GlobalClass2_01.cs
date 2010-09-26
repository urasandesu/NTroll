using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Cecil.DI;
using Urasandesu.NTroll.GlobalClassTestSample;

namespace Test.Urasandesu.NTroll.GlobalClassTestSample.DI
{
    public class GlobalClass2_01 : GlobalClassBase
    {
        protected override GlobalClassBase OnSetup()
        {
            var class2 = new GlobalClass<Class2>();
            class2.Setup(the =>
            {
                the.Method(
                (string value) =>
                {
                    throw new NotImplementedException();
                    return default(string);
                }).
                Instead(_ => _.ReturnTrimedStringIfContainsB);
            });
            return class2;
        }

        protected override string AssemblyCodeBase
        {
            get { return typeof(Class2).Assembly.CodeBase; }
        }

        protected override string AssemblyLocation
        {
            get { return typeof(Class2).Assembly.Location; }
        }
    }
}
