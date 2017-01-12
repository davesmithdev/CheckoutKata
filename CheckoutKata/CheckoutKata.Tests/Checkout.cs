namespace CheckoutKata.Tests
{
    public class Checkout : ICheckout
    {
        private int _totalPrice;

        public void Scan(string item)
        {
            _totalPrice += 50;
        }

        public int GetTotalPrice()
        {
            return _totalPrice;
        }
    }
}