using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Long Value", menuName = "SO/BaseTypes/Long")]
public class LongValue : BaseTypeValue<long> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && long.TryParse(value, out long lVal))
        {
            SetValue(lVal);
        }
        else SetValue(long.Parse(value));
    }
}
