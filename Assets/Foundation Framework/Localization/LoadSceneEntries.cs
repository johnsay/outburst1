using UnityEngine;

namespace FoundationFramework.Language
{
    public class LoadSceneEntries : MonoBehaviour
    {
        [SerializeField] private Localization _prefabFallBack;
        [SerializeField] private TextAsset[] _files;
        [SerializeField] private bool _clearBeforeAdd;
        [SerializeField] private bool _clearOnDestroy;

        private void Awake()
        {
            CheckReady();
            
            AddEntries();
        }

        private void CheckReady()
        {
            if (Localization.IsReady)
                return;

             Instantiate(_prefabFallBack);
        }

        private void AddEntries()
        {
            if(_clearBeforeAdd)Localization.ClearLocalEntries();
		
            Localization.AddLocalEntries(_files);
        }

        private void OnDestroy()
        {
            if(_clearOnDestroy) Localization.ClearLocalEntries();
        }
    }
}


