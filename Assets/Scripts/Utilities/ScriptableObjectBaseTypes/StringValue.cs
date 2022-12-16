using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "String Value", menuName = "SO/BaseTypes/String")]
public class StringValue : BaseTypeValue<string>
{
    public override void SetValue(string value, bool throwErrors)
    {
        SetValue(value);
    }

    public override void SetValue(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
}
