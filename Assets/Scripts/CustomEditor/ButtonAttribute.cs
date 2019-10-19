using UnityEngine;
using System;
using System.Linq;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class ButtonAttribute : Attribute
{
    public string m_buttonName;

    public ButtonAttribute(string _buttonName = "Button")
    {
        m_buttonName = _buttonName;
    }
}
