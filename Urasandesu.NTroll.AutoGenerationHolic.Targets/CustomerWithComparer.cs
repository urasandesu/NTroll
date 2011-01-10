using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;

namespace Urasandesu.NTroll.AutoGenerationHolic.Targets
{
    [EqualityTargetWithComparer]
    public class CustomerWithComparer : IDelegateEqualityComparerable<CustomerWithComparer>
    {
        [EqualityTargetWithComparer]
        public int CustomerKey { get; set; }
        [EqualityTargetWithComparer]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EqualityTargetWithComparer]
        public DateTime ContractDate { get; set; }

        public DelegateEqualityComparer<CustomerWithComparer> Comparer { get; set; }

        public override bool Equals(object obj)
        {
            return Comparer.Equals(this, obj);
        }

        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }
    }
}
