using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Double Value", menuName = "SO/BaseTypes/Double")]
public class DoubleValue : BaseTypeValue<double> 
{
    public override void SetValue(string value, bool throwErrors)
    {
        if (throwErrors && double.TryParse(value, out double dVal))
        {
            SetValue(dVal);
        }
        else SetValue(double.Parse(value));
    }
}