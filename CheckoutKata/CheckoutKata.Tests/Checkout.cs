using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Tests
{
    public class Checkout : ICheckout
    {        
        private readonly List<string> _scannedItems = new List<string>();
        private IProductService _productService { get; set; }
        private IPriceRuleService _priceRuleService { get; set; }

        public Checkout(IProductService productService, IPriceRuleService priceRuleService)
        {
            _productService = productService;
            _priceRuleService = priceRuleService;
        }

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            var prices = _productService.GetProductsAndPrices();
            var priceRules = _priceRuleService.GetPriceRules();

            var totalPrice = _scannedItems.Sum(item => prices[item]);

            foreach (var priceRule in priceRules)
            {
                var numberOfDiscountToApply = NumberOfDiscountsToApply(priceRule);
                var totalDiscountPrice = SingleDiscountAmount(priceRule, prices, priceRules) * numberOfDiscountToApply;
                totalPrice -= totalDiscountPrice;
            }

            return totalPrice;
        }

        private int SingleDiscountAmount(KeyValuePair<string, Tuple<int, int>> priceRule, Dictionary<string, int> prices, Dictionary<string, Tuple<int, int>> priceRules)
        {
            return (ForHowMany(priceRule, priceRules) * ItemPrice(priceRule, prices) - ForHowMuch(priceRule));
        }

        private int NumberOfDiscountsToApply(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return PriceRuleItemTotalScannedCount(priceRule) / NumberOfItemsToScanForDiscountToApply(priceRule);
        }

        private int ForHowMuch(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return priceRule.Value.Item2;
        }

        private int ItemPrice(KeyValuePair<string, Tuple<int, int>> priceRule, Dictionary<string, int> prices)
        {
            return prices[priceRule.Key];
        }

        private int ForHowMany(KeyValuePair<string, Tuple<int, int>> priceRule, Dictionary<string, Tuple<int, int>> priceRules)
        {
            return priceRules[priceRule.Key].Item1;
        }

        private int PriceRuleItemTotalScannedCount(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return _scannedItems.Count(x => x == priceRule.Key);
        }

        private int NumberOfItemsToScanForDiscountToApply(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return priceRule.Value.Item1;
        }
    }
}