
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiButton : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
       
        public delegate void ButtonCallback();

        private ButtonCallback _callback;
        #endregion

        public void Setup(string fieldName,ButtonCallback callback)
        {
            _name.text = fieldName;
            _callback = callback;
        }

        public void PressButton()
        {
            if (_callback != null)
                _callback();
        }

       
    }
}
