using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Urasandesu.NTroll.AutoGenerationHolic.DynamicAssemblySample.Investigation
{
    public class Customer
    {
        int customerKey;
        string firstName;
        string lastName;
        DateTime contractDate;

        public int CustomerKey { get { return customerKey; } set { customerKey = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime ContractDate { get { return contractDate; } set { contractDate = value; } }

        public override bool Equals(object obj)
        {
            var that = default(Customer);
            if ((that = obj as Customer) == null) return false;

            return this.CustomerKey == that.CustomerKey &&
                   this.LastName == that.LastName &&
                   this.ContractDate == that.ContractDate;
        }

        public override int GetHashCode()
        {
            return CustomerKey.GetHashCode() ^
                   (LastName == null ? 0 : LastName.GetHashCode()) ^ 
                   ContractDate.GetHashCode();
        }
    }
}
