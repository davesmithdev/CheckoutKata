using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        private ICheckout _checkout;
        private Mock<IProductService> _productServiceStub { get; set; }
        private Mock<IPriceRuleService> _priceRuleServiceStub { get; set; }

        [SetUp]
        public void Setup()
        {
            _productServiceStub = new Mock<IProductService>();
            _priceRuleServiceStub = new Mock<IPriceRuleService>();

            _productServiceStub.Setup(x => x.GetProductsAndPrices()).Returns(GetProductsAndPrices);
            _priceRuleServiceStub.Setup(x => x.GetPriceRules()).Returns(GetPriceRules);

            _checkout = new Checkout(_productServiceStub.Object, _priceRuleServiceStub.Object);
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

        [Test]
        public void GivenIScanMultipleItemsInRandomOrder_WhenICallGetTotalPrice_TheTotalPriceIs230()
        {
            _checkout.Scan("A");
            _checkout.Scan("C");
            _checkout.Scan("D");
            _checkout.Scan("A");
            _checkout.Scan("B");
            _checkout.Scan("D");
            _checkout.Scan("C");
            _checkout.Scan("A");

            Assert.That(_checkout.GetTotalPrice(), Is.EqualTo(230));
        }

        [Test]
        public void WhenICallGetTotalPrice_ProductPricesAreReturnedFromProductService()
        {
            _checkout.GetTotalPrice();

            _productServiceStub.Verify(x => x.GetProductsAndPrices());
        }

        [Test]
        public void WhenICallGetTotalPrice_PriceRulesAreReturnedFromPriceRuleService()
        {
            _checkout.GetTotalPrice();

            _priceRuleServiceStub.Verify(x => x.GetPriceRules());
        }

        private Dictionary<string, int> GetProductsAndPrices()
        {
            return new Dictionary<string, int>()
            {
                {"A", 50},
                {"B", 30},
                {"C", 20},
                {"D", 15},
            };
        }

        private Dictionary<string, Tuple<int, int>> GetPriceRules()
        {
            return new Dictionary<string, Tuple<int, int>>()
            {
                {"A", new Tuple<int, int>(3, 130)},
                {"B", new Tuple<int, int>(2, 45)}
            };
        }
    }
}