using System;
using NUnit.Framework;
using Urasandesu.NTroll.AutoGenerationHolic.Targets;

namespace Test.Urasandesu.NTroll.AutoGenerationHolic.CodeDomSample
{
    [TestFixture]
    public class ProgramTest
    {
        [Test]
        public void MainTest()
        {
            // データ。
            var customers = new Customer[] 
            { 
                new Customer() { CustomerKey = 1, FirstName = "Mitsuru", LastName = "Nishi", ContractDate = new DateTime(2010, 12, 3) }, 
                new Customer() { CustomerKey = 1, FirstName = "Mitsuru", LastName = "Nishi", ContractDate = new DateTime(2010, 12, 3) }, 
                new Customer() { CustomerKey = 2, FirstName = "Izumiya", LastName = "Masako", ContractDate = new DateTime(2010, 10, 23) }, 
                new Customer() { CustomerKey = 1, FirstName = "Mitsuru", LastName = "Akatani", ContractDate = new DateTime(2010, 12, 3) } 
            };

            // プロパティを使って比較が行われる。
            Assert.IsFalse(object.ReferenceEquals(customers[0], customers[1]));
            Assert.IsTrue(customers[0].Equals(customers[1]));
            Assert.IsTrue(customers[0].GetHashCode() == customers[1].GetHashCode());

            Assert.IsFalse(object.ReferenceEquals(customers[0], customers[2]));
            Assert.IsFalse(customers[0].Equals(customers[2]));
            Assert.IsFalse(customers[0].GetHashCode() == customers[2].GetHashCode());

            
            
            // LastName は比較対象にしていない。
            Assert.IsFalse(object.ReferenceEquals(customers[0], customers[3]));
            Assert.IsTrue(customers[0].Equals(customers[3]));
            Assert.IsTrue(customers[0].GetHashCode() == customers[3].GetHashCode());
        }
    }
}
