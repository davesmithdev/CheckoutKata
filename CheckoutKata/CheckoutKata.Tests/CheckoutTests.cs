using NUnit.Framework;

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
}
