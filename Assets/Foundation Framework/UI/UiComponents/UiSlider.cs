
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiSlider : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
        [SerializeField] private Slider _slider;

        public delegate void SliderCallback(Single state);

        private SliderCallback _callback;
        #endregion

        public void Setup(string fieldName,SliderCallback callback, float minValue, float maxValue)
        {
            _name.text = Language.Localization.Translate(fieldName);
            _slider.minValue = minValue;
            _slider.maxValue = maxValue;
            _callback = callback;
        }

        public void ValueChange(Single state)
        {
            if (_callback != null)
                _callback(state);
        }

        public void SetComponentVisual(Single state)
        {
            
            _slider.value = state;
          
        }
    }
}
