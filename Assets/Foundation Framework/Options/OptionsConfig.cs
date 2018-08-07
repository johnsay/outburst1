using UnityEngine;
using System;

namespace FoundationFramework
{
    [CreateAssetMenu(fileName = "OptionsData", menuName = "Foundation/Create Options data")]
    public class OptionsConfig : ScriptableObject
    {
        #region [[ SOUNDS ]]

        [Header("Sounds Tab")] public bool EnableSoundTab;

        public AudioGroupData[] ExposedAudioGroups;

        #endregion

        #region [[ QUALITY ]]

        [Header("Quality Tab")] public bool EnableQualityTab;
        public bool CanSetResolution;
        public int DefaultResolutionIndex;
        public Vector2[] SupportedResolution;

        public bool CanSetFullScreen;
        public bool CanSetVsync;
        public bool CanSetEconomyMode;
        public bool CanSetTargetFps;

        public int DefaultFpsIndex;
        public int[] SupportedFps;

        #endregion

        #region [[ GAMEPLAY ]]

        [Header("Gameplay Tab")] public bool EnableGameplayTab;
        public bool CansSetDifficulty;
        public string[] Difficulties;

        public bool CanSetSensitiviy;
        #endregion
        
        #region [[ OTHER ]]

        [Header("Other Tab")] public bool EnableOtherTab;
        public UrlData[] UrlLinks;
        public bool CanResetData;

        #endregion

    }

    [Serializable]
    public class AudioGroupData
    {
        public string GroupName;
        public float GroupValue;
    }

    [Serializable]
    public class UrlData
    {
        public string UrlName;
        public string Adress;
    }
    

}