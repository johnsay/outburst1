
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    /// <summary>
    /// setup by profile selection UI
    /// </summary>
    public class ProfileEntryUi : MonoBehaviour
    {
        [SerializeField] private ProfileSelection _profileSelection;
        [SerializeField] private Text _textName;
        public void Setup(string profileName)
        {
            _textName.text = profileName;
        }

        public void ClickProfile()
        {
            _profileSelection.SelectProfile(_textName.text);
            //load card
        }

    }

}
