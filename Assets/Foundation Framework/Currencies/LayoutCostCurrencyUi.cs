using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Collections.Generic;

namespace FoundationFramework
{
    public class LayoutCostCurrencyUi : MonoBehaviour 
    {
        [SerializeField] private LayoutGroup _costLayout;
        [SerializeField] private CostCurrencyUi _costPrefab;
        private GenericUiList<CurrencyAmount> _cost;

        public void Initialize()
        {
            if(_cost == null)
            _cost = new GenericUiList<CurrencyAmount>(_costPrefab.gameObject,_costLayout);
        }

        private void Awake()
        {
            Initialize();
        }
        

        public void SetupItemCosts(IEnumerable<CurrencyAmount> data)
        {

            if (_cost == null) return;
            _cost.Generate<CostCurrencyUi>(data, (entry, price) =>
            {
                price.Setup(entry);
            });
        }
    }
}


