using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextSOUpdater<T> : MonoBehaviour where T : IComparable<T>, IEquatable<T>
{
    [SerializeField] protected BaseTypeValue<T> value;

    [SerializeField] protected Text text;
    [SerializeField] protected string prefix, suffix;

    [Tooltip("optional feeding back to value")]
    [SerializeField] private InputField inputText;

    protected virtual void Start()
    {
        if (!value)
        {
            Debug.LogError($"[TextSOUpdater] Object {name} has no reference to BaseTypeValue of type {typeof(T)}");
            return;
        }

        if(inputText)
        {
            inputText.text = value.Value.ToString();
            inputText.onEndEdit.AddListener(OnEndEdit);
        }

        value.OnValueChanged.AddListener(OnValueChange);
        text.text = prefix + value.ToString() + suffix;
    }

    protected virtual void OnEndEdit(string textValue)
    {
        value.OnValueChanged.RemoveListener(OnValueChange);
        value.SetValue(text.text, false);
        value.OnValueChanged.AddListener(OnValueChange);
    }

    protected virtual void OnValueChange(T newValue)
    {
        if(inputText)
        {
            inputText.onEndEdit.RemoveListener(OnEndEdit);
            inputText.text = newValue.ToString();
        }
        text.text = prefix + newValue.ToString() + suffix;

        if (inputText)
        {
            inputText.onEndEdit.AddListener(OnEndEdit);
        }

    }

    protected virtual void OnValidate()
    {
        if (!text) text = GetComponent<Text>();
    }
}
