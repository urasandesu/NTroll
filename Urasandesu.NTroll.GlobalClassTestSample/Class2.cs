using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.GlobalClassTestSample
{
    public class Class2
    {
        public string ReturnTrimedStringIfContainsB(string value)
        {
            if (value.ToLower().Contains('b'))
            {
                return value.Trim();
            }
            else
            {
                return value;
            }
        }

        public string ReturnReversedStringIfContainsC(string value)
        {
            if (value.ToLower().Contains('c'))
            {
                return new string(EnumerateAndReverse(value).ToArray());
            }
            else
            {
                return value;
            }
        }

        IEnumerable<char> EnumerateAndReverse(string value)
        {
            for (int i = value.Length - 1; i >= 0; i--)
            {
                yield return value[i];
            }
        }
    }
}
