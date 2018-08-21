using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using FoundationFramework.Language;

public class LocalizationEditorWindow : EditorWindow
{
	#region FIELDS
	private Language _currentLanguage = Language.English;
	private string _currentKey;
	private Vector2 _scrollPos = Vector2.zero;
	private static Dictionary<string,Dictionary<Language,string>> _cachedEntries = new Dictionary<string, Dictionary<Language, string>>();
	private static LocalizationContainer _container;
	private GUIStyle _keyField;
	private GUIStyle _toolBarStyle;
	private bool _stylesNotLoaded = true;
	private static bool _opened;
	#endregion
	
	private void LoadStyles()
	{
		_toolBarStyle = new GUIStyle("LargeButtonMid")
		{
			alignment = TextAnchor.MiddleCenter,
			richText = true,
			fontSize = 18
		};

		_keyField = new GUIStyle(EditorStyles.boldLabel)
		{
			alignment = TextAnchor.MiddleLeft,
			richText = true,
			fontSize = 14
		};

		_stylesNotLoaded = false;
	}
	
    public static void ShowWindow(LocalizationContainer container)
    {
	    if (_opened) return;
	    if (EditorApplication.isCompiling) return;
	    if (container == null) return;
	    Debug.Log("opening localization database:"+container.name);
	    _container = container;
	    _container.Load();
	    _cachedEntries = _container.Entries;
		GetWindow<LocalizationEditorWindow>().Show();
	    _opened = true;
    }

	private void OnDestroy()
	{
		if (_container == null) return;
		_container.Save();
		AssetDatabase.Refresh();
		UnityEditor.EditorUtility.SetDirty(_container);
		AssetDatabase.SaveAssets();
		_opened = false;
	}

	

    private void OnGUI()
    {
	    if (EditorApplication.isCompiling)
	    {
		    if (_opened)
		    {
			    GetWindow<LocalizationEditorWindow>().Close();
			    _opened = false;
		    }
		    return;
	    }
	    if (_opened == false)
	    {
		    EditorGUILayout.LabelField("Editor Compiling, please reopen the window", _toolBarStyle, GUILayout.Width(600), GUILayout.Height(90));
		    return;
	    }

	    if(_stylesNotLoaded)LoadStyles();
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height));
		GUILayout.BeginVertical();

		DrawHeaders();
	    EditorUtility.DrawLargeLine();
		DrawAllData();
		DrawAddButton();

		GUILayout.EndVertical();

		EditorGUILayout.EndScrollView();
    }

	private void DrawHeaders()
    {
		GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Key", _toolBarStyle, GUILayout.Width(100), GUILayout.Height(30));
        GUILayout.Space(20);
        EditorGUILayout.LabelField("Translation", _toolBarStyle, GUILayout.Width(150), GUILayout.Height(30));
	    GUILayout.Space(20);
	    int enumInt = (int)_currentLanguage;
	    EditorUtility.DrawEnumOfType(typeof(Language),ref enumInt);
	    _currentLanguage = (Language) enumInt;
        GUILayout.EndHorizontal();
    }

    private void DrawAllData()
	{
		var deleteKey =  string.Empty;
	
		foreach(var data in _cachedEntries)
		{
			GUILayout.Space(10);
			GUILayout.BeginHorizontal();
			GUILayout.Space(20);
			EditorGUILayout.LabelField(data.Key,_keyField, GUILayout.Width(100), GUILayout.Height(30));
			GUILayout.Space(20);
			data.Value[_currentLanguage] = EditorGUILayout.TextArea( data.Value[_currentLanguage], GUILayout.Height(30));
			GUILayout.Space(20);
			GUI.backgroundColor = Color.red;
			if(GUILayout.Button("Delete", GUILayout.Width(100), GUILayout.Height(30))) deleteKey = data.Key;
			GUI.backgroundColor = Color.white;
			GUILayout.EndHorizontal();
			GUILayout.Space(10);
			EditorUtility.DrawThinLine();
		}

		if( string.IsNullOrEmpty(deleteKey) == false) _cachedEntries.Remove(deleteKey);
	
		if(_cachedEntries.Count>0) _container.Entries = _cachedEntries;
	}

    private void DrawAddButton()
	{
		GUILayout.Space(20);
		GUILayout.BeginHorizontal();
		GUILayout.Space(20);
		EditorGUILayout.LabelField("Add New key =>", EditorStyles.boldLabel, GUILayout.Width(120), GUILayout.Height(20));
		_currentKey = EditorGUILayout.TextField("",_currentKey, GUILayout.Width(200), GUILayout.Height(20));
		GUILayout.Space(20);
		GUI.backgroundColor = Color.green;
		if (GUILayout.Button("Add", GUILayout.Width(100), GUILayout.Height(20)))
		{
			if (string.IsNullOrEmpty(_currentKey)) return;
				
			var locEntries = _container.Entries;
			var newdict = new Dictionary<Language, string>();
			
	        foreach (var language in (Language[]) Enum.GetValues(typeof(Language)))  newdict.Add(language,_currentKey);
	    
			locEntries.Add(_currentKey,newdict);
	        _currentKey = string.Empty;
        }
		
		GUILayout.EndHorizontal();
	}

    private void OnInspectorUpdate()
    {
	    if (_opened == false) return;
	    if (EditorApplication.isCompiling) return;
        Repaint();
    }
 
  
}
