using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    public interface IProductService
    {
        Dictionary<string, int> GetProductsAndPrices();
    }
}