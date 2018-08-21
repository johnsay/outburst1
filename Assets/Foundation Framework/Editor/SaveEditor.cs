using UnityEditor;
using UnityEngine;


namespace FoundationFramework
{
    public class SaveEditor
    {   
        private const string DebugTag = "[Foundation]  ";
        public static void OpenDataFolder()
        {
            if (IoUtils.IsDirectoryExist(DataPath.Get) == false)
            {
                Debug.LogWarning(DebugTag+"The save folder is not created yet.");
            }
            else
            {
                UnityEditor.EditorUtility.RevealInFinder(DataPath.Get); 
            }

            
        }

        public static void ClearSaveFolder()
        {
            bool success = FileUtil.DeleteFileOrDirectory(DataPath.Get);
            if (success)
            {
                Debug.Log(DebugTag+"Deleted the save folder.");
            }
            else
            {
                Debug.LogWarning(DebugTag+"The save folder is not found.");
            }
        }
    }
}
