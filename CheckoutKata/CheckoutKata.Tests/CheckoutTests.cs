using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void GivenIScanItemA_WhenICallGetTotalPrice_TheTotalPriceIs50()
        {
            ICheckout checkout = new Checkout();

            checkout.Scan("A");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }
    }

    public class Checkout : ICheckout
    {
        public void Scan(string item)
        {
            throw new NotImplementedException();
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }
    }

    public interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
