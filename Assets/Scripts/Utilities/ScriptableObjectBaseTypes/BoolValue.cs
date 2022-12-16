using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Bool Value", menuName = "SO/BaseTypes/Bool")]
public class BoolValue : BaseTypeValue<bool> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && bool.TryParse(value, out bool bVal))
        {
            SetValue(bVal);
        }
        else SetValue(bool.Parse(value));
    }
}