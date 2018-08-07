using UnityEditor;
using UnityEngine;

namespace FoundationFramework.Editor
{
    public class EditorMenu
    {

        public static void OpenFoundationDocumentation()
        {
            Application.OpenURL("https://tinytreegames.com/foundation_documentation/");
        }
        
        public static void OpenDeveloperWebsite()
        {
            Application.OpenURL("https://tinytreegames.com/");
        }
        
        public static void AskQuestionByEmail()
        {
            Application.OpenURL("mailto:delmarle.damien@gmail.com?subject=[Foundation-Framework]Question");
        }

        
        public static void PingInProject(string path, string asset = "")
        {

            if (path[path.Length - 1] == '/') path = path.Substring(0, path.Length - 1);
            
            var obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }


    }
}


