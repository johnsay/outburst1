using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FoundationFramework.Editor
{
    public static class ButtonsEditorExtensions
    {
        public static void DrawEditorButtons(this UnityEditor.Editor editor)
        {
            GUILayout.Space(20);
            var methods = editor.target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);
            foreach (var method in methods)
            {
                var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

                if (ba != null)
                {

                    GUI.enabled = ba.Mode == ButtonMode.AlwaysEnabled
                        || (EditorApplication.isPlaying ? ba.Mode == ButtonMode.EnabledInPlayMode : ba.Mode == ButtonMode.DisabledInPlayMode);

                    var buttonName = string.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;
                    if(ba.Color != Color.clear)  GUI.backgroundColor = ba.Color;
                    
                    if (GUILayout.Button(buttonName,GUILayout.Width(200), GUILayout.Height(40)))
                    {
                        foreach (var t in editor.targets)  method.Invoke(t, null);
                    }
                    GUILayout.Space(20);
                    GUI.enabled = true;
                }
            }
        }
    }
}
