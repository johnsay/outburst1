
namespace FoundationFramework
{
    /// <summary>
    /// abstract handler to be extended
    /// </summary>
    public class DataHandler : Initializable
    {
        protected string FileName = "";
        protected IContainer DataContainer;
       
        protected bool IsLoaded;

        protected virtual void BuildDefaultData()
        {
            
        }

        public override void Initialize()
        {
            BuildDefaultData();
            IContainer container = DataStorageGenerator.GenerateDataStorage(); 
            container.SetPath(DataPath.Get + FileName);           
            DataContainer = container;
            if (!System.IO.File.Exists(DataPath.Get + FileName))
            {
                SaveData();
            }
            LoadData();
        }

        public void DeleteData()
        {
            DataContainer.Delete();
        }

        #region JSON Data Saving & Loading
        public virtual void SaveData()
        {
        }

        public virtual void LoadData()
        {      
        }
        #endregion
    }
}


