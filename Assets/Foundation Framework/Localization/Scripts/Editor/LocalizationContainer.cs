using UnityEngine;
using System;
using System.Collections.Generic;
using FoundationFramework.Editor;
using UnityEditor;

namespace FoundationFramework.Language
{
    [Serializable]
    [CreateAssetMenu(fileName = FileName, menuName =  EditorUtility.CreateAssetPath+FileName)]
    public class LocalizationContainer : ScriptableObject
    {
        #region FIELDS
        private const string FileName = "LocalizationDB";
        [SerializeField] private TextAsset _textFile;
        private ISerializer _serializer;
        private IStorageAccess _storage;
        private string _jsonContent;
        public Dictionary<string, Dictionary<Language, string>> Entries = new Dictionary<string, Dictionary<Language, string>>();
        #endregion

        private void OnEnable()
        {
            _serializer = new JsonSerializer();
            _storage = new FileStorageAccess();
        }

        public void Load()
        {
            OnEnable();
            if (string.IsNullOrEmpty(_jsonContent)) return;
            
            Entries = _serializer.Unserialize<Dictionary<string, Dictionary<Language, string>>>(_jsonContent);
            if (Entries == null) Entries = new Dictionary<string, Dictionary<Language, string>>();
        }

        public void Save()
        {
            var data = _serializer.Serialize(Entries);

            if (data.Length > 2)
                _jsonContent = data;
        }

        #region BUTTONS
        [Button(ButtonColor.Green)]
        public void EditContent()
        {
            LocalizationEditorWindow.ShowWindow(this);
        }

        [Button(ButtonColor.Red)]
        public void ResetContent()
        {
            _jsonContent = string.Empty;
            Entries = new Dictionary<string, Dictionary<Language, string>>();
        }

        [Button(ButtonColor.Yellow)]
        public void ExportToTextFile()
        {
            string path = AssetDatabase.GetAssetPath(this);
            if (_textFile)
            {
                path = AssetDatabase.GetAssetPath(_textFile);
            }

            path = path.Replace(".asset", ".txt");
            _storage.Write(_jsonContent,path);
           
            Debug.Log(path);
            AssetDatabase.Refresh();
        }
        
        [Button(ButtonColor.Yellow)]
        public void ImportFromTextFile()
        {
           
            if (_textFile)
            {
                var path = AssetDatabase.GetAssetPath(_textFile);
                var content = _storage.Read(path);
                Debug.Log(content.Length + " characters exported");
                _jsonContent = content;
            }
            else
            {
                Debug.LogWarning("No export file selected");
            }
        }
        #endregion
    }
}
