
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public class OwnedCurrencyUi : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private CurrencyType _currency;
        [SerializeField] private CurrencyDataHandler _dataHandler;
        [SerializeField] private Text _text;
        [SerializeField] private Image _icon;

        #endregion

        public void OnOpen()
        {
            _dataHandler.OnCurrencyChange += OnCurrencyChange;
            OnCurrencyChange();
        }

        public void OnClose()
        {
            _dataHandler.OnCurrencyChange -= OnCurrencyChange;
        }

        private void OnCurrencyChange()
        {
            _text.text = _dataHandler.GetCurrencyAmount(_currency.name).ToString();
            _icon.sprite = _currency.Icon;
        }
    }
}
