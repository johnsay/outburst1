using UnityEngine;

namespace FoundationFramework
{
    /// <summary>
    /// save and load the last used profile name
    /// </summary>
    public class ProfileDataHandler : DataHandler
    {
        [SerializeField] private UseProfileDataHandler[] _dataChilds;
        public ProfileData CachedProfileData { get; private set; }
        
        protected override void BuildDefaultData()
        {
            FileName = "ProfileSave";
            CachedProfileData = new ProfileData();
        }

        public override void Initialize()
        {
#if !DOTWEEN
            Debug.LogError("Framework not setup, open menu: Tools/Foundation Framework/ToolBar");
#if UNITY_EDITOR
            UnityEditor. EditorApplication.ExecuteMenuItem("Edit/Play");
            Application.Quit();
#endif
#endif
                      
            base.Initialize();
            foreach (var data in _dataChilds)
            {
                data.InitializedByProfile(this);
            }
        }

        public override void SaveData()
        {
            DataContainer.Save(CachedProfileData);
        }

        public override void LoadData()
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                var tempData = DataContainer.Load<ProfileData>();
                if (tempData != null)
                {
                    CachedProfileData = tempData;
                }
            }    
        }
    }
}

