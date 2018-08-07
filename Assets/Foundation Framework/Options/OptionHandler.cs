using UnityEngine;

namespace FoundationFramework
{
    public class OptionHandler : DataHandler
    {
        [SerializeField] private OptionsConfig _config;
        
        public OptionData CachedOptionData { get; private set; }
        
        protected override void BuildDefaultData()
        {
            FileName = "OptionSave";
            CachedOptionData = new OptionData();
            CachedOptionData.InitializeSave(_config);
        }

        public override void SaveData()
        {
            DataContainer.Save(CachedOptionData);
        }

        public override void LoadData()
        {
            if (IsLoaded) return;
            
            IsLoaded = true;
            var tempData = DataContainer.Load<OptionData>();
            if (tempData != null)
            {
                CachedOptionData = tempData;
            }
        }
    }
}
