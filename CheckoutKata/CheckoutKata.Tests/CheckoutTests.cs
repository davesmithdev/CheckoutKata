using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private ICheckout _checkout;

        [SetUp]
        public void Setup()
        {
            _checkout = new Checkout();
        }

        [Test]
        public void GivenIScanItemA_WhenICallGetTotalPrice_TheTotalPriceIs50()
        {
            _checkout.Scan("A");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void GivenIScanItemATwoTimes_WhenICallGetTotalPrice_TheTotalPriceIs100()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(100));
        }
    }
}