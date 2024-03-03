
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

[CreateAssetMenu(menuName = "Variables/GameObject Variable")]
public class GameObjectVariable : VariableObject<GameObject>
{
    public GameObjectVariable()
    {
        onChanged = new UnityEvent<GameObject>();
    }

    public GameObjectVariable(GameObject value)
    {
        Value = value;
    }

    public void SetValue(GameObjectVariable value)
    {
        Value = value.Value;
        onChanged.Invoke(Value);
    }

    public static implicit operator GameObject(GameObjectVariable reference)
    {
        return reference.Value;
    }
}

#if UNITY_EDITOR
 [CustomEditor(typeof(GameObjectVariable))]
 public class GameObjectVariableEditor : VariableObjectEditor<GameObject>{}
 #endif