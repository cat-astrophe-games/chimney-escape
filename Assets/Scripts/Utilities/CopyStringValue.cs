using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyStringValue : MonoBehaviour
{
    [SerializeField] private StringValue value;

    public void Copy()
    {
        GUIUtility.systemCopyBuffer = value;
    }
}
