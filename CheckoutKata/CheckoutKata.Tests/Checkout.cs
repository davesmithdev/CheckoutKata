using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    public class Checkout : ICheckout
    {
        private int _totalPrice;
        private List<string> _scannedItems = new List<string>();

        public void Scan(string item)
        {
            _scannedItems.Add(item);
        }

        public int GetTotalPrice()
        {
            foreach (var item in _scannedItems)
            {
                _totalPrice += (item == "A") ? 50 : 30;
            }

            return _totalPrice;
        }
    }
}