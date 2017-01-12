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

        [Test]
        public void GivenIScanItemB_WhenICallGetTotalPrice_TheTotalPriceIs30()
        {
            _checkout.Scan("B");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(30));
        }

        [Test]
        public void GivenIScanItemC_WhenICallGetTotalPrice_TheTotalPriceIs20()
        {
            _checkout.Scan("C");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(20));
        }

        [Test]
        public void GivenIScanItemD_WhenICallGetTotalPrice_TheTotalPriceIs15()
        {
            _checkout.Scan("D");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(15));
        }

        [Test]
        public void GivenIScanItemAThreeTimes_WhenICallGetTotalPrice_TheDiscountIsAppliedAndTheTotalPriceIs130()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(130));
        }

        [Test]
        public void GivenIScanItemBTwoTimes_WhenICallGetTotalPrice_TheDiscountIsAppliedAndTheTotalPriceIs45()
        {
            _checkout.Scan("B");
            _checkout.Scan("B");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(45));
        }

         [Test]
        public void GivenIScanItemASixTimesAndItemBFourTimes_WhenICallGetTotalPrice_MultipleDiscountsAreAppliedAndTheTotalPriceIs350()
        {
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("B");
            _checkout.Scan("B");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(350));
        }
    }
}