
using UnityEngine;

namespace FoundationFramework.Language
{
    public class UiChangeLanguage : MonoBehaviour
    {
        [SerializeField] private Language _language;
        
        public void TrigerLanguageChange()
        {
            Localization.ChangeLanguage(_language);
        }
    }
}

