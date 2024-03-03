// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

[Serializable]
public class GameObjectReference
{
    public bool UseConstant = true;
    public GameObject ConstantValue;
    public GameObjectVariable Variable;

    public GameObjectReference(){}

    public GameObjectReference(GameObject value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public GameObject Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator GameObject(GameObjectReference reference)
    {
        return reference.Value;
    }

    public void AddListener(UnityEngine.Events.UnityAction<GameObject> function)
    {
        Variable.onChanged.AddListener(function);
    }

    public void RemoveListener(UnityEngine.Events.UnityAction<GameObject> function)
    {
        Variable.onChanged.AddListener(function);
    }
}