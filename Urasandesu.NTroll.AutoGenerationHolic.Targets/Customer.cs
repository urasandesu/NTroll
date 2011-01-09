using System;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;

namespace Urasandesu.NTroll.AutoGenerationHolic.Targets
{
    [EqualityTarget]
    public class Customer
    {
        [EqualityTarget]
        public int CustomerKey { get; set; }
        [EqualityTarget]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EqualityTarget]
        public DateTime ContractDate { get; set; }
    }
}
