using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Float Value", menuName = "SO/BaseTypes/Float")]
public class FloatValue : BaseTypeValue<float> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && float.TryParse(value, out float fVal))
        {
            SetValue(fVal);
        }
        else SetValue(float.Parse(value));
    }
}