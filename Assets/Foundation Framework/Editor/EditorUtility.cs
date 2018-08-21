using UnityEngine;
using UnityEditor;
using System;

public static class EditorUtility 
{
    private const string EditorAssetsPath = "Assets/Foundation Framework/Editor/Assets/";
    public const string CreateAssetPath = "Foundation/";

    public static void DrawEnumOfType(Type genericType, ref int data)
    {
        if (!genericType.IsEnum) return;
        
        string s = Enum.GetName(genericType, data);
        string[] names = Enum.GetNames(genericType);
        int index = 0;
        for (int i = 0; i < names.Length; i++) if (names[i] == s) index = i;
        index = EditorGUILayout.Popup("", index, names, GUILayout.Width(120), GUILayout.Height(30));
        object item = Enum.Parse(genericType, names[index]);
        data = Convert.ToInt32(item);
    }
	
    public static void DrawThinLine()
    {

        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
    }

    public static void DrawLargeLine()
    {
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
    }
    
    public static Texture2D GetEditorTexture(string textureName)
    {
        return EditorGUIUtility.Load(EditorAssetsPath + textureName + ".png") as Texture2D;
    }

    public static bool ButtonPressed(string buttonName, string buttonIcon = null)
    {
        var buttonContent = new GUIContent("   "+buttonName,GetEditorTexture(buttonIcon));
            
        return GUILayout.Button(buttonContent, EditorStyles.label);
    }
    
    public static bool ButtonPressed(string buttonName,Color buttonColor, string buttonIcon = null)
    {
        GUI.backgroundColor = buttonColor;
        var buttonContent = new GUIContent("   "+buttonName,GetEditorTexture(buttonIcon));
        GUI.backgroundColor = Color.white;
        return GUILayout.Button(buttonContent, EditorStyles.label);
    }
}
