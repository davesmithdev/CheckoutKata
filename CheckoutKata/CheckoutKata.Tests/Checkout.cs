using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckoutKata.Tests
{
    public class Checkout : ICheckout
    {        
        private readonly List<string> _scannedItems = new List<string>();
        private readonly Dictionary<string, int> _prices = new Dictionary<string, int>()
        {
            {"A", 50},
            {"B", 30},
            {"C", 20},
            {"D", 15}
        };
        private readonly Dictionary<string, Tuple<int, int>> _priceRules = new Dictionary<string, Tuple<int, int>>()
        {
            {"A", new Tuple<int, int>(3, 130)},
            {"B", new Tuple<int, int>(2, 45)}
        }; 

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            var totalPrice = _scannedItems.Sum(item => _prices[item]);

            foreach (var priceRule in _priceRules)
            {
                var numberOfDiscountToApply = NumberOfDiscountsToApply(priceRule);
                var totalDiscountPrice = SingleDiscountAmount(priceRule) * numberOfDiscountToApply;
                totalPrice -= totalDiscountPrice;
            }

            return totalPrice;
        }

        private int SingleDiscountAmount(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return (ForHowMany(priceRule) * ItemPrice(priceRule) - ForHowMuch(priceRule));
        }

        private int NumberOfDiscountsToApply(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return PriceRuleItemTotalScannedCount(priceRule) / NumberOfItemsToScanForDiscountToApply(priceRule);
        }

        private int ForHowMuch(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return priceRule.Value.Item2;
        }

        private int ItemPrice(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return _prices[priceRule.Key];
        }

        private int ForHowMany(KeyValuePair<string, Tuple<int, int>> priceRule)
        {
            return _priceRules[priceRule.Key].Item1;
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