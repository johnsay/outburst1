
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.UI
{
    public class UiDropDown : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private Text _name;
        [SerializeField] private Dropdown _dropdown;
        private DropDownCallback _callback;
        public delegate void DropDownCallback(Int32 state);
        #endregion
        
        public void Setup(string fieldName,DropDownCallback callback, List<string> entries)
        {
            _name.text = Language.Localization.Translate(fieldName);
            _dropdown.options.Clear();
            _callback = callback;
            _dropdown.AddOptions(entries);
        }

        public void OnValueChanged(Int32 index)
        {
            if (_callback != null)
                _callback(index);
        }

    }

}

