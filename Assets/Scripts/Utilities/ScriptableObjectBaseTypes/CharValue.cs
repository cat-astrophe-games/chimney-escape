using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Char Value", menuName = "SO/BaseTypes/Char")]
public class CharValue : BaseTypeValue<char> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && char.TryParse(value, out char cVal))
        {
            SetValue(cVal);
        }
        else SetValue(char.Parse(value));
    }
}