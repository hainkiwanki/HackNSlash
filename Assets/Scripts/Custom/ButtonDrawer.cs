using System;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using UnityEngine;

#if UNITY_EDITOR
[UnityEditor.CanEditMultipleObjects]
[UnityEditor.CustomEditor(typeof(UnityEngine.Object), true)]
public class ObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawButtons();
        DrawDefaultInspector();
    }

    void DrawButtons()
    {
        Editor editor = this;
        var methods = editor.target.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(m => m.GetParameters().Length == 0);

        foreach (var method in methods)
        {
            // Get the ButtonAttribute on the method (if any)
            var ba = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));

            if (ba != null)
            {
                // Draw a button which invokes the method
                var buttonName = String.IsNullOrEmpty(ba.m_buttonName) ? ObjectNames.NicifyVariableName(method.Name) : ba.m_buttonName;
                if (GUILayout.Button(buttonName))
                {
                    foreach (var t in editor.targets)
                    {
                        method.Invoke(t, null);
                    }
                }
            }
        }
    }
}
#endif