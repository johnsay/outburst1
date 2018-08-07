using System;
using System.Collections.Generic;

namespace FoundationFramework
{
    [Serializable]
    public class CurrenciesData
    {
        public Dictionary<string, int> Currencies = new Dictionary<string, int>();
        public bool IsInitialized = false;
    }
}

