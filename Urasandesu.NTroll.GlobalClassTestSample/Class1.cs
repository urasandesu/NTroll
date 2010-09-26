using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.GlobalClassTestSample
{
    public class Class1
    {
        public string DoNothing(string value)
        {
            return value;
        }

        public string Return3TimesIfValueContainsA(string value)
        {
            if (value.ToLower().Contains('a'))
            {
                return value + value + value;
            }
            else
            {
                return value;
            }
        }
    }
}
