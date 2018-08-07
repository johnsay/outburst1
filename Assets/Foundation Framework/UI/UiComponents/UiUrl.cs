
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiUrl : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
        [SerializeField] private Image _icon;

        private string _urlAdress;
       
        public delegate void ButtonCallback(string url);

        private ButtonCallback _callback;
        #endregion

        public void Setup(UrlData data, ButtonCallback callback)
        {
            if (_name) _name.text = data.UrlName;
           // if (_icon) _icon.sprite = data.Icon;
           // if (_icon) _icon.color = data.Icon ? Color.white : Color.clear;
            _urlAdress = data.Adress;
            _callback = callback;
        }

        public void PressButton()
        {
            if (_callback != null)
                _callback(_urlAdress);
        }

       
    }
}
