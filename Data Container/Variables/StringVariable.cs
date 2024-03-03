
﻿// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Variables/String Variable")]
public class StringVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
    public bool resetOnPlay;
    public string defaultValue;
#endif
    public string Value;
    public UnityEvent<string> onChanged;

    public StringVariable()
    {
        onChanged = new UnityEvent<string>();
    }

    public void SetValue(string value)
    {

        Value = value;
        onChanged.Invoke(Value);
    }

    public void SetValue(StringVariable value)
    {
        Value = value.Value;
        onChanged.Invoke(Value);
    }

    public static implicit operator string(StringVariable reference)
    {
        return reference.Value;
    }

        public static StringVariable operator +(StringVariable stringVariable, string variable)
    {
        stringVariable.SetValue(stringVariable.Value + variable);
        return stringVariable;
    }
}

#if UNITY_EDITOR
 [CustomEditor(typeof(StringVariable))]
 public class StringVariableEditor : VariableObjectEditor<string>{}
 #endif