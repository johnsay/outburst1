using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework.Language
{
    public class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private Text _text;

        private void Start()
        {
            OnUpdated();
        }

        private void OnEnable()
        {
            Localization.OnUpdated += OnUpdated;
            OnUpdated();
        }

        private void OnDisable()
        {
            Localization.OnUpdated -= OnUpdated;
        }

        private void OnUpdated()
        {
            if(Localization.IsReady)
            _text.text = Localization.Translate(_key);
        }
    }

}

