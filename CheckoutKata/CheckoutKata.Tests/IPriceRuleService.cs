using System;
using System.Collections.Generic;

namespace CheckoutKata.Tests
{
    public interface IPriceRuleService
    {
        Dictionary<string, Tuple<int, int>> GetPriceRules();
    }
}