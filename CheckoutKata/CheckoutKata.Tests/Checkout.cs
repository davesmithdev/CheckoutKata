using System;
using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    public class Checkout : ICheckout
    {
        private int _totalPrice;
        private List<string> _scannedItems = new List<string>();
        private Dictionary<string, int> _prices = new Dictionary<string, int>()
        {
            {"A", 50},
            {"B", 30},
            {"C", 20}
        };

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            foreach (var item in _scannedItems)
            {
                _totalPrice += _prices[item];
            }

            return _totalPrice;
        }
    }
}