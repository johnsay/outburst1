
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiToggle : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
        [SerializeField] private Toggle _toggle;

        public delegate void ToggleCallback(bool state);

        private ToggleCallback _callback;
        #endregion

        public void Setup(string fieldName,ToggleCallback callback)
        {
            _name.text = Language.Localization.Translate(fieldName);
            _callback = callback;
        }

        public void SwitchToggle(bool state)
        {
            if (_callback != null)
                _callback(state);
        }

        public void SetComponentState(bool state)
        {
            var events = _toggle.onValueChanged;
            _toggle.onValueChanged = null;
            _toggle.isOn = state;
            _toggle.onValueChanged = events;
        }
    }
}
