
using FoundationFramework.Language;
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiButton : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
        private string _key;
        public delegate void ButtonCallback();

        private ButtonCallback _callback;
        #endregion
        private void OnDisable()
        {
            Localization.OnUpdated -= OnUpdated;
        }
        
        public void Setup(string fieldName,ButtonCallback callback)
        {
            _name.text = fieldName;
            _callback = callback;
        }
        
        public void SetupTranslated(string fieldName,ButtonCallback callback)
        {
            _key = fieldName;
            _name.text = Localization.Translate(fieldName);;
            _callback = callback;
            Localization.OnUpdated += OnUpdated;
        }

        public void PressButton()
        {
            if (_callback != null)
                _callback();
        }
        
        private void OnUpdated()
        {
            if(Localization.IsReady)
                _name.text = Localization.Translate(_key);
        }

       
    }
}
