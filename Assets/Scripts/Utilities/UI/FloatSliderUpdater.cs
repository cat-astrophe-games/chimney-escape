using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class FloatSliderUpdater : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private Slider slider;

    [SerializeField] private FloatValue currentValue;
    
    [SerializeField] private FloatValue maxValue;

    [ReadOnly]
    [SerializeField] private bool willSaveToSO;

    private void OnValidate()
    {
        if (!slider) slider = GetComponent<Slider>();
        willSaveToSO = maxValue == null;
    }

    private void Start()
    {
        slider.wholeNumbers = false;
        if (maxValue)
            slider.maxValue = maxValue;
        slider.value = currentValue;

        currentValue.OnValueChanged.AddListener(UpdateCurrentValue);
        if (maxValue)
            maxValue.OnValueChanged.AddListener(UpdateMaxValue);
    }

    private void OnDestroy()
    {
        currentValue.OnValueChanged.RemoveListener(UpdateCurrentValue);
        if (maxValue)
            maxValue.OnValueChanged.RemoveListener(UpdateMaxValue);
    }

    private void UpdateCurrentValue(float newValue)
    {
        slider.value = newValue;
    }

    private void UpdateMaxValue(float newValue)
    {
        slider.maxValue = newValue;
    }

    private void SaveCurrentValue(float value)
    {
        currentValue.SetValue(value);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!maxValue)
        {
            SaveCurrentValue(slider.value);
        }
    }
}
