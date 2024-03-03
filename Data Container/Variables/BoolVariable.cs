
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

[CreateAssetMenu(menuName = "Variables/Bool Variable")]
public class BoolVariable : VariableObject<bool>
{

    public BoolVariable()
    {
        onChanged = new UnityEvent<bool>();
    }

    public BoolVariable(bool value)
    {
        Value = value;
    }

    public void SetValue(BoolVariable value)
    {
        Value = value.Value;
        onChanged.Invoke(Value);
    }

    public static implicit operator bool(BoolVariable reference)
    {
        return reference.Value;
    }
}

#if UNITY_EDITOR
 [CustomEditor(typeof(BoolVariable))]
 public class BoolVariableEditor : VariableObjectEditor<bool>{}
 #endif