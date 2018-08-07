using UnityEngine;

namespace FoundationFramework
{
    public class UiTabHandler : MonoBehaviour
    {
        [SerializeField] private UiTab _defaultOpenTab;
        [SerializeField]private UiTab[] _tabs;
        private UiTab _currentTab;

       

        private void Awake()
        {
            for (int i = 0; i < _tabs.Length; i++)
            {
                _tabs[i].SetState(false);
            }
        }

        private void TryClosePreviousTab()
        {
            if (_currentTab)
                _currentTab.SetState(false);
        }

        public void OnOpenPanel()
        {
            OpenTab(_defaultOpenTab);
        }

        public void OpenTab(UiTab tab)
        {
            TryClosePreviousTab();
            _currentTab = tab;
            _currentTab.SetState(true);
        }

    }
}
