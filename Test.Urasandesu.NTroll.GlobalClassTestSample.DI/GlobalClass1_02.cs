using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NAnonym.Cecil.DI;
using Urasandesu.NTroll.GlobalClassTestSample;

namespace Test.Urasandesu.NTroll.GlobalClassTestSample.DI
{
    public class GlobalClass1_02 : GlobalClassBase
    {
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
