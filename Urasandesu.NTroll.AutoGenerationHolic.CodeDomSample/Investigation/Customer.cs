using System;
using Urasandesu.NTroll.AutoGenerationHolic.Helpers;

namespace Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample.Investigation
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
            if (obj == null) return false;
            if (!typeof(Customer).IsAssignableFrom(obj.GetType())) return false;

            Customer that = (Customer)obj;
            return this.CustomerKey == that.CustomerKey &&
                   this.FirstName == that.FirstName &&
                   this.ContractDate == that.ContractDate;
        }

        public override int GetHashCode()
        {
            return Operators.Eor(
                        Operators.Eor(
                            Operators.NullableGetHashCode(this.CustomerKey),
                            Operators.NullableGetHashCode(this.FirstName)
                        ),
                        Operators.NullableGetHashCode(this.ContractDate)
                   );
        }
    }
}
