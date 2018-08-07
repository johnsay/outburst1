using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace FoundationFramework.UI
{
    public class OptionWindow : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private OptionsConfig _config;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private UiPanelBase _panel;

        [Header("Prefabs: ")] 
        [SerializeField] private UiSlider _prefabSlider;
        [SerializeField] private UiToggle _prefabToggle;
        [SerializeField] private UiDropDown _prefabDropdown;
        [SerializeField] private UiButton _prefabButton;
        [SerializeField] private UiUrl _prefabUrl;

        [Header("Tabs:")] 
        [SerializeField] private UiTabHandler _tabHandler;
        [SerializeField] private UiTab _tabSounds;
        [SerializeField] private UiTab _tabQuality;
        [SerializeField] private UiTab _tabGameplay;
        [SerializeField] private UiTab _tabOther;

        [SerializeField] private OptionHandler _save;
      
        private UiSlider[] _cachedGroupVolumes;

        private const float MinAudioLevel = -45;
        private const float MaxAudioLevel = 20;
        #endregion

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _save.Initialize();
            
            SetupSoundsTab();

            SetupQualityTab();

            SetupGameplayTab();

            SetupOtherTab();
           
            LoadSavedValues();
            
        }

        #region [[ SETUP ]]

        private void SetupSoundsTab()
        {
            if (_config.EnableSoundTab)
            {
                List<UiSlider> volumesSliders = new List<UiSlider>();
                for (int i = 0; i < _config.ExposedAudioGroups.Length; i++)
                {
                    var slider = Instantiate(_prefabSlider, _tabSounds.transform);
                    volumesSliders.Add(slider);
          

                    var index = i;
                    slider.Setup(_config.ExposedAudioGroups[i].GroupName, val =>
                    {
                        OnSetGroupVolume(val,_config.ExposedAudioGroups[index].GroupName);
                    },MinAudioLevel,MaxAudioLevel);
                }
                _cachedGroupVolumes = volumesSliders.ToArray();
            }
        }

        private void SetupQualityTab()
        {
            if (_config.EnableQualityTab)
            {
                if (_config.CanSetResolution)
                {
                    var resolutionDropdown = Instantiate(_prefabDropdown, _tabQuality.transform);
                    List<string> listResolutions = new List<string>();
                
                    for (int i = 0; i < _config.SupportedResolution.Length; i++)
                    {
                        string convertedResolution = (int)_config.SupportedResolution[i].x+" x "+(int)_config.SupportedResolution[i].y;
                        listResolutions.Add(convertedResolution);
                    }

                    string windowedTranslation = Language.Localization.Translate("Resolutions");
                    resolutionDropdown.Setup(windowedTranslation,OnSetResolution,listResolutions);
               
                }

                if (_config.CanSetFullScreen)
                {
                    var setFullScreenToggle = Instantiate(_prefabToggle,_tabQuality.transform);
                    string fullScreenTranslation = Language.Localization.Translate("Full Screen");
                    setFullScreenToggle.Setup(fullScreenTranslation,OnSetFullScreen);
                }
                
                if (_config.CanSetVsync)
                {
                    var setVsyncToggle = Instantiate(_prefabToggle,_tabQuality.transform);
                    string vsyncTranslation = Language.Localization.Translate("Use Vsync");
                    setVsyncToggle.Setup(vsyncTranslation,OnSetVsync);
                }
                
                if (_config.CanSetEconomyMode)
                {
                    var setEcoModeToggle = Instantiate(_prefabToggle,_tabQuality.transform);
                    string ecoModeTranslation = Language.Localization.Translate("Economy mode");
                    setEcoModeToggle.Setup(ecoModeTranslation,OnSetEconomyMode);
                }
                
                if (_config.CanSetTargetFps)
                {
                    var fpsDropdown = Instantiate(_prefabDropdown, _tabQuality.transform);
                    List<string> listResolutions = new List<string>();
                
                    for (int i = 0; i < _config.SupportedResolution.Length; i++)
                    {
                        string convertedFps = _config.SupportedFps[i].ToString();
                        listResolutions.Add(convertedFps);
                    }

                    string targetFpsTranslation = Language.Localization.Translate("Refresh rate");
                    fpsDropdown.Setup(targetFpsTranslation,OnSetTargetFps,listResolutions);
               
                }
            }

        }

        private void SetupGameplayTab()
        {
            if (_config.EnableGameplayTab)
            {
                if (_config.CansSetDifficulty)
                {
                    var difficultyDropDown = Instantiate(_prefabDropdown, _tabGameplay.transform);
                    List<string> listDifficulties = new List<string>();
                
                    for (int i = 0; i < _config.Difficulties.Length; i++)
                    {
                        string convertedDifficulty = _config.Difficulties[i];
                        listDifficulties.Add(convertedDifficulty);
                    }

                    string difficultyTranslation = Language.Localization.Translate("Difficulty");
                    difficultyDropDown.Setup(difficultyTranslation,OnSetDifficulty,listDifficulties);
               
                }
                
                if (_config.CanSetSensitiviy)
                {
                    var slider = Instantiate(_prefabSlider, _tabGameplay.transform);
                    string sensitivityTranslation = Language.Localization.Translate("Sensitivity");
                    slider.Setup(sensitivityTranslation,OnSetSensitivity,0.1f,2);
                }
            }
        }

        private void SetupOtherTab()
        {
            if (_config.EnableOtherTab)
            {
                if (_config.CanResetData)
                {
                    var button = Instantiate(_prefabButton, _tabOther.transform);
                    string resetTranslation = Language.Localization.Translate("Reset Data");
                    button.Setup(resetTranslation,OnResetData);
                }

                for (int i = 0; i < _config.UrlLinks.Length; i++)
                {
                    var url = _config.UrlLinks[i];
                    
                    var prefab = Instantiate(_prefabUrl, _tabOther.transform);
                    
                    prefab.Setup(url,OnOpenUrl);
                }
            }
        }

        #endregion
        
        #region [[ LOAD ]]

        private void LoadSavedValues()
        {
            _save.LoadData();

            LoadDataAudioGroups();
            LoadDataQuality();
            LoadGameplayData();
        }

        private void LoadDataAudioGroups()
        {
            for (int i = 0; i < _cachedGroupVolumes.Length; i++)
            {
                _cachedGroupVolumes[i].SetComponentVisual(_save.CachedOptionData.SoundGroupValues[_config.ExposedAudioGroups[i].GroupName]);
            }
        }

        private void LoadDataQuality()
        {
            if(_config.CanSetResolution)
               OnSetResolution(_save.CachedOptionData.ResolutionValue);
            
            if(_config.CanSetFullScreen)
                OnSetFullScreen(_save.CachedOptionData.FullScreenValue);
            
            if(_config.CanSetVsync)
                OnSetVsync(_save.CachedOptionData.VsyncValue != 0);
            
            if(_config.CanSetEconomyMode)
                OnSetEconomyMode(_save.CachedOptionData.EconomyModeValue);
            
            if(_config.CanSetTargetFps)
                OnSetTargetFps(_save.CachedOptionData.FpsIndex);
        }

        private void LoadGameplayData()
        {
            if(_config.CansSetDifficulty)
                OnSetTargetFps(_save.CachedOptionData.DifficultyValue);

            if (_config.CanSetSensitiviy)
            {
                OnSetSensitivity(_save.CachedOptionData.SensitivityValue);
            }
        }

        #endregion

        #region [[ SOUNDS TAB CALLBACK
        private void OnSetGroupVolume(float val, string group)
        {
            _mixer.SetFloat(group, val);
            _save.CachedOptionData.TrySetSoundGroupValue(group, val);
            _save.SaveData();
        }
        #endregion
       
        #region [[QUALITY TAB CALLBACKS ]]
        private void OnSetResolution(int resolutionIndex)
        {
            var resolution = _config.SupportedResolution[resolutionIndex];
            Screen.SetResolution((int)resolution.x,(int)resolution.y,true);
            _save.CachedOptionData.ResolutionValue = resolutionIndex;
        }

        private void OnSetFullScreen(bool state)
        {
            Screen.fullScreen = state;
            _save.CachedOptionData.FullScreenValue = state;
        }

        private void OnSetVsync(bool state)
        {
            QualitySettings.vSyncCount = state ? 4 : 0;
            _save.CachedOptionData.VsyncValue = state ? 4 : 0;
        }

        private void OnSetEconomyMode(bool state)
        {
            if (state)
            {
            }
            else
            {
            }

            _save.CachedOptionData.EconomyModeValue = state;
        }

        private void OnSetTargetFps(int fpsIndex)
        {
            Application.targetFrameRate = _config.SupportedFps[fpsIndex];
            _save.CachedOptionData.FpsIndex = fpsIndex;
        }

        #endregion
        
        #region [[ GAMEPLAY TAB CALLBACKS ]]

        private void OnSetDifficulty(int difficulty)
        {
            _save.CachedOptionData.DifficultyValue = difficulty;
        }
        
        private void OnSetSensitivity(float sensitivity)
        {
            _save.CachedOptionData.SensitivityValue = sensitivity;
        }

        #endregion
        
        #region [[ OTHER TAB CALLBACKS ]]

        private void OnResetData()
        {
            _save.CachedOptionData.InitializeSave(_config);
            _save.SaveData();
            LoadSavedValues();
        }

        private void OnOpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        #endregion
      
        public void OpenTab(UiTab tab)
        {
            _tabHandler.OpenTab(tab);
        }

        public void OpenWindow()
        {
            _tabHandler.OnOpenPanel();
            _panel.Show();
        }
    }

}
