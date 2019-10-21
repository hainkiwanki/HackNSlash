using UnityEngine;
using System.Runtime.CompilerServices;
public static class Logger
{

    public static void LogError(string _msg,
        Color _color,
        [CallerLineNumber] int _lineNr = 0,
        [CallerMemberName] string _caller = null,
        [CallerFilePath] string _path = null)
    { 
        
        Debug.LogError($"<color=#{ColorUtility.ToHtmlStringRGBA(_color)}>[{_caller} at line {_lineNr}] Error:</color>{_msg}");
    }
}