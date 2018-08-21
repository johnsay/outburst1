using System;
using UnityEngine;

namespace FoundationFramework.Editor
{
    public enum ButtonMode
    {
        AlwaysEnabled,
        EnabledInPlayMode,
        DisabledInPlayMode
    }
    
    public enum ButtonColor
    {
        Red,
        Green,
        Yellow
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class ButtonAttribute : Attribute
    {
        private string _name;
        private Color _color;
        private ButtonMode mode = ButtonMode.AlwaysEnabled;

        public string Name { get { return _name; } }
        public ButtonMode Mode { get { return mode; } }
        public Color Color { get { return _color; } }
        public ButtonAttribute()
        {
        }
        
        public ButtonAttribute(ButtonColor color)
        {
            switch (color)
            {
                case ButtonColor.Red:
                    _color = Color.red;
                    break;
                case ButtonColor.Green:
                    _color = Color.green;
                    break;
                case ButtonColor.Yellow:
                    _color = Color.yellow;
                    break;
               
            }
        }

        public ButtonAttribute(string name)
        {
            _name = name;
        }
        
        public ButtonAttribute(string name,Color color)
        {
            _name = name;
            _color = color;
        }

        public ButtonAttribute(string name, ButtonMode mode,Color color)
        {
            _name = name;
            _color = color;
            this.mode = mode;
        }

        public ButtonAttribute(ButtonMode mode)
        {
            this.mode = mode;
        }
    }
}
