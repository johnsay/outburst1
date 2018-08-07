using System;

namespace FoundationFramework
{
    /// <summary>
    /// use this as a field when you want to define how much an item cost, how much a monster give gold ...
    /// </summary>
    [Serializable]
    public class CurrencyAmount
    {
        public CurrencyType Currency;
        public int Count;
    }
}
