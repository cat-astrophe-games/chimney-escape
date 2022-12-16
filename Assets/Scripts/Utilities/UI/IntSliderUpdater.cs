using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class IntSliderUpdater : MonoBehaviour
{
    [SerializeField] private IntValue currentValue, maxValue;
    
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.wholeNumbers = true;
        slider.maxValue = maxValue;
        slider.value = currentValue;

        currentValue.OnValueChanged.AddListener(UpdateCurrentValue);
        maxValue.OnValueChanged.AddListener(UpdateMaxValue);
    }

    private void OnDestroy()
    {
        currentValue.OnValueChanged.RemoveListener(UpdateCurrentValue);
        maxValue.OnValueChanged.RemoveListener(UpdateMaxValue);
    }

    private void UpdateCurrentValue(int newValue)
    {
        slider.value = newValue;
    }

    private void UpdateMaxValue(int newValue)
    {
        slider.maxValue = newValue;
    }
}
