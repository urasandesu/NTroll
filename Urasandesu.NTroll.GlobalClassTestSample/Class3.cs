using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.GlobalClassTestSample
{
    public class Class3
    {
        public string Display(string value)
        {
            string result = value;
            if (result.ToLower().Contains('a'))
            {
                result = new Class1().Return3TimesIfValueContainsA(result);
            }

            if (result.ToLower().Contains('b'))
            {
                result = new Class2().ReturnTrimedStringIfContainsB(result);
            }

            if (result.ToLower().Contains('c'))
            {
                result = new Class2().ReturnReversedStringIfContainsC(result);
            }

            return new Class1().DoNothing(result);
        }
    }
}
