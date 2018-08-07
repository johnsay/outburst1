using UnityEngine;

namespace FoundationFramework
{
    [CreateAssetMenu(fileName = "DefaultCurrencies", menuName = "Foundation/Create starting Currencies values")]
    public class CurrencyDefaultAmount : ScriptableObject
    {
        public CurrencyAmount[] Currencies;
    }
}


