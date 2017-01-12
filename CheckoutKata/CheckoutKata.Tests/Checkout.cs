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

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            var totalPrice = _scannedItems.Sum(item => _prices[item]);

            if (_scannedItems.Count(i => i == "A") == 3)
                totalPrice -= 20;

            return totalPrice;
        }
    }
}