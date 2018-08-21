using UnityEngine;

namespace FoundationFramework.Language
{
    public class LocalizationDataHandler : DataHandler
    {
        public LocalizationData CachedLocalizationData { get; private set; }
	
        protected override void BuildDefaultData()
        {
            FileName = "LocalizationSave";
            CachedLocalizationData = new LocalizationData();
        }
        
        public override void SaveData()
        {
            DataContainer.Save(CachedLocalizationData);
        }

        public override void LoadData()
        {
            if (!IsLoaded)
            {
                IsLoaded = true;
                var tempData = DataContainer.Load<LocalizationData>();
                if (tempData != null)
                {
                    CachedLocalizationData = tempData;
                }
            }    
        }

        public Language FindLanguage 
        {
            get
            {
                if (CachedLocalizationData.IsDefault)
                {
                    switch (Application.systemLanguage)
                    {
                        case SystemLanguage.Afrikaans:
                            break;
                        case SystemLanguage.Arabic:
                            break;
                        case SystemLanguage.Basque:
                            break;
                        case SystemLanguage.Belarusian:
                            break;
                        case SystemLanguage.Bulgarian:
                            break;
                        case SystemLanguage.Catalan:
                            break;
                        case SystemLanguage.Chinese:
                            return Language.ChineseTraditional;
                        case SystemLanguage.Czech:
                            break;
                        case SystemLanguage.Danish:
                            break;
                        case SystemLanguage.Dutch:
                            break;
                        case SystemLanguage.English:
                            return Language.English;
  
                        case SystemLanguage.Estonian:
                            break;
                        case SystemLanguage.Faroese:
                            break;
                        case SystemLanguage.Finnish:
                            break;
                        case SystemLanguage.French:
                            return Language.French;
                        case SystemLanguage.German:
                            break;
                        case SystemLanguage.Greek:
                            break;
                        case SystemLanguage.Hebrew:
                            break;
                        case SystemLanguage.Hungarian:
                            break;
                        case SystemLanguage.Icelandic:
                            break;
                        case SystemLanguage.Indonesian:
                            break;
                        case SystemLanguage.Italian:
                            break;
                        case SystemLanguage.Japanese:
                            break;
                        case SystemLanguage.Korean:
                            break;
                        case SystemLanguage.Latvian:
                            break;
                        case SystemLanguage.Lithuanian:
                            break;
                        case SystemLanguage.Norwegian:
                            break;
                        case SystemLanguage.Polish:
                            break;
                        case SystemLanguage.Portuguese:
                            break;
                        case SystemLanguage.Romanian:
                            break;
                        case SystemLanguage.Russian:
                            break;
                        case SystemLanguage.SerboCroatian:
                            break;
                        case SystemLanguage.Slovak:
                            break;
                        case SystemLanguage.Slovenian:
                            break;
                        case SystemLanguage.Spanish:
                            return Language.Spanish;
                        case SystemLanguage.Swedish:
                            break;
                        case SystemLanguage.Thai:
                            break;
                        case SystemLanguage.Turkish:
                            break;
                        case SystemLanguage.Ukrainian:
                            break;
                        case SystemLanguage.Vietnamese:
                            break;
                        case SystemLanguage.ChineseSimplified:
                            break;
                        case SystemLanguage.ChineseTraditional:
                            return Language.ChineseTraditional;
                        case SystemLanguage.Unknown:
                            break;
                        default:
                            return Language.English;
                    }  
                }
                else
                {
                    return CachedLocalizationData.SavedLanguage;
                }

                return Language.English;
            }
        }

        public void SaveLanguage(Language language)
        {
            CachedLocalizationData.IsDefault = false;
            CachedLocalizationData.SavedLanguage = language;
            SaveData();
        }
    }
}
