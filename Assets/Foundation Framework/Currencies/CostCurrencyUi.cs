using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public class CostCurrencyUi : MonoBehaviour
    {
        [SerializeField] private Text _amount;
        [SerializeField] private Image _icon;

        public void Setup(CurrencyAmount info)
        {
            _amount.text = info.Count.ToString();
            _icon.sprite = info.Currency.Icon;
        }

    }
}


