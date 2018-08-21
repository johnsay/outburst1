
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
    [Serializable]
    public class OptionData
    {
        public bool Initialized;
        
        #region [[ SOUNDS ]]
        public Dictionary<string, float> SoundGroupValues = new Dictionary<string, float>();
        #endregion
        #region [[ QUALITY ]]
        public int ResolutionValue;
        public bool FullScreenValue;
        public int VsyncValue;
        public bool EconomyModeValue;
        public int FpsIndex;
        #endregion
        #region [[ GAMEPLAY ]]
        public int DifficultyValue;
        public float SensitivityValue;
        #endregion

        public void InitializeSave(OptionsConfig config)
        {
            if (config.EnableSoundTab)
            {
                SoundGroupValues.Clear();
                for (int i = 0; i < config.ExposedAudioGroups.Length; i++)
                {
                    var current = config.ExposedAudioGroups[i];
                    SoundGroupValues.Add(current.GroupName,current.GroupValue);
                }
            }

            if (config.CanSetResolution) ResolutionValue = config.DefaultResolutionIndex;
          
            if (config.CanSetFullScreen)  FullScreenValue = Screen.fullScreen;

            if (config.CanSetVsync) VsyncValue = QualitySettings.vSyncCount;

            if (config.CanSetEconomyMode) EconomyModeValue = false;

            if (config.CanSetTargetFps) FpsIndex = config.DefaultFpsIndex;

            if (config.CansSetDifficulty) DifficultyValue = 0;

            if (config.CanSetSensitiviy) SensitivityValue = 1;

            Initialized = true;

        }

        public void TrySetSoundGroupValue(string group, float value)
        {
            if (SoundGroupValues.ContainsKey(group))
            {
                SoundGroupValues[group] = value;
            }
            else
            {
                SoundGroupValues.Add(group, value);
            }
        }
    }

}

