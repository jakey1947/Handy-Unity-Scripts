
﻿// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Variables/Float Variable")]
public class FloatVariable : VariableObject<float>
{
    public UnityEvent<float, bool> onChangedSigned;

    public FloatVariable()
    {
        onChanged = new UnityEvent<float>();
        onChangedSigned = new UnityEvent<float, bool>();
    }

        public FloatVariable(float value)
    {
        onChanged = new UnityEvent<float>();
        onChangedSigned = new UnityEvent<float, bool>();
        Value = value;
    }

    public override void SetValue(float value)
    {
        bool positiveChange = value > Value;
        Value = value;
        onChangedSigned.Invoke(Value, positiveChange);
        onChanged.Invoke(Value);
    }

    public void ApplyChange(float amount)
    {
        bool positiveChange = amount > 0;
        Value += amount;
        onChangedSigned.Invoke(Value, positiveChange);
        onChanged.Invoke(Value);
    }

    public static implicit operator float(FloatVariable reference)
    {
        return reference.Value;
    }

    public static FloatVariable operator +(FloatVariable floatVariable, float variable)
    {
        floatVariable.ApplyChange(variable);
        return floatVariable;
    }

    public static FloatVariable operator -(FloatVariable floatVariable, float variable)
    {
        floatVariable.ApplyChange(-variable);
        return floatVariable;
    }
}

#if UNITY_EDITOR
 [CustomEditor(typeof(FloatVariable))]
 public class FloatVariableEditor : VariableObjectEditor<float>{}
 #endif