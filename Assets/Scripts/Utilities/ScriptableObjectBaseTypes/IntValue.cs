using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Int Value", menuName = "SO/BaseTypes/Int")]
public class IntValue : BaseTypeValue<int> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && int.TryParse(value, out int iVal))
        {
            SetValue(iVal);
        }
        else SetValue(int.Parse(value));
    }
}