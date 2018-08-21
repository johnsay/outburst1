//
//Icons from: http://www.fatcow.com/free-icons
//
using UnityEngine;
using UnityEditor;

namespace FoundationFramework.Editor
{
    public class SupportWindow : EditorWindow
    {
        private const string Version = "Foundation Version 1.1.0";
        
        private const string StoreAdress = "content/117861";
        private const string WindowName = "Toolbar";
        private const string MenuPath = "Tools/Foundation Framework/Open Toolbar";
        private const string DotweenAddress = "content/27676";
        
        
        GUIStyle _publisherNameStyle;
        #if DOTWEEN
        GUIStyle _toolBarStyle;
        GUILayoutOption _toolbarHeight;
    #endif
       
        GUIContent[] _toolbarOptions;
        int _toolBarIndex;
        GUIStyle _greyText;
        GUIStyle _centeredVersionLabel;
        GUIStyle _reviewBanner;
        GUILayoutOption _bannerHeight;

        bool _stylesNotLoaded = true;

        [MenuItem(MenuPath)]
        static void ShowWindow()
        {
            SupportWindow myWindow = GetWindow<SupportWindow>(WindowName);
            myWindow.minSize = new Vector2(300, 400);
            myWindow.maxSize = myWindow.minSize;
            myWindow.titleContent = new GUIContent(WindowName);
            myWindow.Show();
        }

        private void OnEnable()
        {
            _toolbarOptions = new GUIContent[2];
            _toolbarOptions[0] = new GUIContent("<size=14><b> Tools</b></size>\n <size=11>Utilities to simplify \n your workflow.</size>",EditorUtility.GetEditorTexture("cog"), "");
            _toolbarOptions[1] = new GUIContent("<size=14><b> Links</b></size>\n <size=11>Documentation \n and support.</size>", EditorUtility.GetEditorTexture("link"), "");
#if DOTWEEN
            _toolbarHeight = GUILayout.Height(50);
    #endif
            _bannerHeight = GUILayout.Height(30);
        }

        void LoadStyles()
        {
            _publisherNameStyle = new GUIStyle(EditorStyles.label)
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };
#if DOTWEEN
            _toolBarStyle = new GUIStyle("LargeButtonMid")
            {
                alignment = TextAnchor.MiddleLeft,
                richText = true
            };
#endif
            _greyText = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleLeft
            };

            _centeredVersionLabel = new GUIStyle(EditorStyles.centeredGreyMiniLabel)
            {
                alignment = TextAnchor.MiddleCenter,
            };

            _reviewBanner = new GUIStyle("TL SelectionButton")
            {
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            _stylesNotLoaded = false;
        }

        void OnGUI()
        {
            if (_stylesNotLoaded) LoadStyles();

            EditorGUILayout.Space();
            GUILayout.Label(new GUIContent("<size=20><b><color=#666666>Foundation Framework</color></b></size>"), _publisherNameStyle);
            EditorGUILayout.Space();

           #if !DOTWEEN
                EditorGUILayout.Space();
                if (ButtonPressed("[1]: Import Dotween package"))
                    UnityEditorInternal.AssetStore.Open(DotweenAddress);
                EditorGUILayout.LabelField(" click here to access package.", _greyText);

                EditorGUILayout.Space();
                if (ButtonPressed("[2]: Setup DoTween"))
                   Debug.Log(" Open Tools/Demigiant/Dotween Utility panel.");
                EditorGUILayout.LabelField(" Open Tools/Demigiant/Dotween Utility panel.", _greyText);
                
                EditorGUILayout.Space();
                if (ButtonPressed("[3]: Enable Dotween Integration"))
                {
                    string Directive = "DOTWEEN";
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Facebook,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.tvOS,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Facebook,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.PS4,Directive);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.XboxOne,Directive);
                    Debug.Log("Adding the scripting define symbol "+Directive);
                    SupportWindow currentWindow = GetWindow<SupportWindow>(WindowName);
                    currentWindow.Close();
    
                }
                EditorGUILayout.LabelField(" to enable the logic.", _greyText);
            
             #else
            _toolBarIndex = GUILayout.Toolbar(_toolBarIndex, _toolbarOptions, _toolBarStyle, _toolbarHeight);
           
            switch (_toolBarIndex)
            {
                case 0:
                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Open save folder","open_save"))
                        SaveEditor.OpenDataFolder();
                    EditorGUILayout.LabelField(" Play First to create the folder.", _greyText);

                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Delete all saves","folder_delete"))
                        SaveEditor.ClearSaveFolder();
                    EditorGUILayout.LabelField(" Data folder + all profiles.", _greyText);

                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Go to Scenes","unity"))
                        EditorMenu.PingInProject("Assets/Foundation Framework/Scenes");
                    EditorGUILayout.LabelField(" Select the scenes folder.", _greyText);
                    break;

                case 1:
                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Send an email","mail"))
                        EditorMenu.AskQuestionByEmail();
                    EditorGUILayout.LabelField(" Ask any question here.", _greyText);

                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Documentation","docs"))
                        EditorMenu.OpenFoundationDocumentation();
                    EditorGUILayout.LabelField(" All modules explained.", _greyText);

                    EditorGUILayout.Space();
                    if (EditorUtility.ButtonPressed("Developer Website","tiny_tree"))
                        EditorMenu.OpenDeveloperWebsite();
                    EditorGUILayout.LabelField(" Games & assets made.", _greyText);
                    break;
            }
            #endif

           

            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField(new GUIContent(Version), _centeredVersionLabel);
            EditorGUILayout.Space();
            if (GUILayout.Button(new GUIContent("<size=11> Please consider leaving us a review.</size>",EditorUtility.GetEditorTexture("star"), ""), _reviewBanner, _bannerHeight))
                UnityEditorInternal.AssetStore.Open(StoreAdress);
        }
        
        #region Utilities

       

        #endregion
    }
}