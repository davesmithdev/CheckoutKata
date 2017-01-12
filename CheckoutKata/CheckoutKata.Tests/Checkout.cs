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
                var numberOfDiscountsToApply = _scannedItems.Count(x => x == priceRule.Key) / priceRule.Value.Item1;
                var discountPrice = (_priceRules[priceRule.Key].Item1 *  _prices[priceRule.Key]) - priceRule.Value.Item2;
            }

            return totalPrice;
        }
    }
}