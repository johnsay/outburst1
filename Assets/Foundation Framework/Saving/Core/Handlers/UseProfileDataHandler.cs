using UnityEngine;

namespace FoundationFramework
{
    public class UseProfileDataHandler : DataHandler
    {
        [SerializeField] private bool _saveOnDestroy;
        private ProfileDataHandler _profileHandler;
        
        public virtual void InitializedByProfile(ProfileDataHandler profileHandler)
        {
            _profileHandler = profileHandler;
             Initialize();   
        }

        public override void Initialize()
        {
            BuildDefaultData();
            IContainer container = DataStorageGenerator.GenerateDataStorage();
            var completePath = DataPath.Get + "/" + _profileHandler.CachedProfileData.LastUsedProfile + "/" + FileName;
            container.SetPath(completePath);           
            DataContainer = container;
            if (!System.IO.File.Exists(completePath))
            {
                SaveData();
            }
            LoadData();
        }
        
        #region AUTOSAVING
        private void OnDestroy()
        {
            if(_saveOnDestroy) SaveData();
        }
        #endregion
    }
}
