
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

public abstract class VariableObject<T> : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
    public bool resetOnPlay;
    public T defaultValue;
#endif
    public T Value;
    public UnityEvent<T> onChanged;


    public VariableObject()
    {
        onChanged = new UnityEvent<T>();
    }

    public virtual void SetValue(T value)
    {
        Value = value;
        onChanged.Invoke(Value);
    }

    public virtual void SetValue(VariableObject<T> value)
    {
        Value = value.Value;
        onChanged.Invoke(Value);
    }

    public static implicit operator T(VariableObject<T> reference)
    {
        return reference.Value;
    }
}

#if UNITY_EDITOR
 [CustomEditor(typeof(VariableObject<>))]
 public class VariableObjectEditor<T> : Editor
 {
    public VariableObjectEditor()
    {
        EditorApplication.playModeStateChanged += onPlayEvent;
    }
    private void onPlayEvent(PlayModeStateChange state)
    {
        var script = (VariableObject<T>)target;

        if(!script.resetOnPlay) return;

        script.Value = script.defaultValue;
    }  

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        VariableObject<T> script = (VariableObject<T>)target;

        if(GUILayout.Button("Update", GUILayout.Height(40)))
        {
            script.onChanged.Invoke(script.Value);
        }
    }
 }
 #endif