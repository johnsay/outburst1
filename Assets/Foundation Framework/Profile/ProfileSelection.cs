using UnityEngine;
using FoundationFramework.UI;

namespace FoundationFramework
{
    public class ProfileSelection : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private ProfileDataHandler _profileDataHandler;
        [SerializeField] private ProfileSelectionUi _profileUi;
        [SerializeField] private string _nextSceneToEnter = "Hub";
        #endregion
        
        private void Start()
        {
            _profileDataHandler.Initialize();
            var lastProfile = _profileDataHandler.CachedProfileData.LastUsedProfile;
            if (string.IsNullOrEmpty(lastProfile))
            {
                _profileUi.InitialOpenProfileCreation();
            }
            else
            {
                InitialOpenProfileList();
            }
        }

        private void InitialOpenProfileList()
        {
            var selected = _profileDataHandler.CachedProfileData.LastUsedProfile;
            _profileUi.SetProfileList(_profileDataHandler.CachedProfileData.List);
            if(string.IsNullOrEmpty(selected) == false)
                SelectProfile(selected);
            _profileUi.InitialOpenProfileList();
        }

        private void OpenProfileList()
        {
            var selected = _profileDataHandler.CachedProfileData.LastUsedProfile;
            _profileUi.SetProfileList(_profileDataHandler.CachedProfileData.List);
            if(string.IsNullOrEmpty(selected) == false)
            SelectProfile(selected);
            _profileUi.OpenProfileList();
        }

        public void SelectProfile(string profileName)
        {
            _profileDataHandler.CachedProfileData.LastUsedProfile = profileName;
            _profileUi.StartSelectedSequence(profileName);
        }

        public void DeleteProfile()
        {
           var profileName = _profileDataHandler.CachedProfileData.LastUsedProfile;
            
           var dialogData = DialogBoxData.CreateActionBox
           (
               "Delete the save "+profileName+" ?",
               () =>
               {
                   _profileDataHandler.CachedProfileData.RemoveProfile(profileName);
                   _profileDataHandler.SaveData();
                   OpenProfileList();
               },
               () =>
               {
                   //Cancel
               },
               "Delete" 
           );
            
            UiDialogBox.Instance.ShowDialog(dialogData);
           
        }

        public void EnterProfile()
        {
            SceneLoader.Instance.LoadScene(_nextSceneToEnter);
            _profileDataHandler.SaveData();
            
        }

        public void CheckCanCreateProfile()
        {
            
            if (_profileDataHandler.CachedProfileData.CanAddProfile() == false)
            {
                var msg = DialogBoxData.CreateInfo("Max profile reached. \n Delete one first.");
                UiDialogBox.Instance.ShowDialog(msg);
                return;
            }
            
            _profileUi.OpenProfileCreation(); 
        }

        public void TryCreateProfile(string profileName)
        {
            
            if (_profileDataHandler.CachedProfileData.ProfileExist(profileName))
            {
                var msg = DialogBoxData.CreateInfo("You already have a profile \n with this name.");
                UiDialogBox.Instance.ShowDialog(msg);
                return;
            }
            _profileDataHandler.CachedProfileData.AddNewProfile(profileName);
            _profileDataHandler.SaveData();
            OpenProfileList();
        }
    }
}

