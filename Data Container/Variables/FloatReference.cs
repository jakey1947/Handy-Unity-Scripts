// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;


[Serializable]
public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public FloatReference()
    { }

    public FloatReference(float value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }

    public void AddListener(UnityEngine.Events.UnityAction<float> function)
    {
        Variable.onChanged.AddListener(function);
    }

    public void RemoveListener(UnityEngine.Events.UnityAction<float> function)
    {
        Variable.onChanged.AddListener(function);
    }

    public void AddListenerSigned(UnityEngine.Events.UnityAction<float,bool> function)
    {
        Variable.onChangedSigned.AddListener(function);
    }

    public void RemoveListenerSigned(UnityEngine.Events.UnityAction<float,bool> function)
    {
        Variable.onChangedSigned.AddListener(function);
    }
}