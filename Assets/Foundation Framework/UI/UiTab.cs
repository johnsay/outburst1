using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public class UiTab : MonoBehaviour 
    {
        #region [[ FIELDS ]] 

        [SerializeField] private CanvasGroup _canvas;
        [SerializeField] private Button _openTabButton;

        #endregion


        public virtual void SetState(bool open)
        {
            _canvas.alpha = open ? 1 : 0;
            _canvas.blocksRaycasts = open;
            _canvas.interactable = open;

            if (_openTabButton) _openTabButton.interactable = !open;
        }

    }
}
