using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleUpdater : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private BoolValue boolValue;

    private void Start()
    {
        toggle.isOn = boolValue;
        toggle.onValueChanged.AddListener(ValueUpdate);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(ValueUpdate);
    }

    private void ValueUpdate(bool value)
    {
        boolValue.SetValue(value);
    }

    private void OnValidate()
    {
        if (!toggle)
            toggle = GetComponent<Toggle>();
    }
}
