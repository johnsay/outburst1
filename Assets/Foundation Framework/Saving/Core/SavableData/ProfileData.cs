using System.Collections.Generic;
using System;

namespace FoundationFramework
{
    [Serializable]
    public class ProfileData
    {
        private const int MaxProfiles = 3;
        public List<string> List = new List<string>();
        public string LastUsedProfile;

        public bool ProfileExist(string profileName)
        {
            return List.Contains(profileName);
        }

        public void AddNewProfile(string profileName)
        {
            List.Add(profileName);
            LastUsedProfile = profileName;
        }

        public void RemoveProfile(string profileName)
        {
            List.Remove(profileName);

            TryRemoveFolder(profileName);

            if (LastUsedProfile == profileName)
                LastUsedProfile = List.Count > 0 ? List[List.Count-1]:string.Empty;
		
        }

        public bool CanAddProfile()
        {
            return List.Count < MaxProfiles;
        }

        private void TryRemoveFolder(string profileName)
        {
            IoUtils.TryDeleteFolder(DataPath.Get+"/"+profileName);
        }
    }

}

