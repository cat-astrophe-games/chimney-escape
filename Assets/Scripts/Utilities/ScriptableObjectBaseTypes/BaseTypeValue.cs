using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public abstract class BaseTypeValue<T> : ScriptableObject, ISaveable
{
    [SerializeField]
    private T value;
    public T Value
    {
        get => value;
        set
        {
            if (!EqualityComparer<T>.Default.Equals(this.value, value))
            {
                this.value = value;
                Save();
                onValueChanged?.Invoke(this.value);
                onValueChangedNoData?.Invoke();
            }
        }
    }

    [SerializeField] private bool canSave;
    [SerializeField] private T startValue;
    [SerializeField] private UnityEvent<T> onValueChanged;
    [SerializeField] private UnityEvent onValueChangedNoData;

    public bool debug { get; set; }
    public bool CanSave => canSave;
    public T StartValue => startValue;

    public UnityEvent<T> OnValueChanged => onValueChanged;

    public UnityEvent OnValueChangedNoData => onValueChangedNoData;

    public static implicit operator T(BaseTypeValue<T> value)
    {
        return value.Value;
    }

    public override string ToString()
    {
        return value.ToString();
    }

    public abstract void SetValue(string value, bool throwErrors);
    

    public virtual void SetValue(T value)
    {
        Value = value;
    }

    public virtual void OnValidate()
    {
        OnValueChanged?.Invoke(value);
    }

    public virtual void Save()
    {
        if (canSave)
        {
            if(debug)
            {
                Debug.Log($"[{base.ToString()}] saving playerPrefs under key {name} with value {ToString()}");
            }
            PlayerPrefs.SetString(name, ToString());
        }
    }

    public virtual void Load()
    {
        if (canSave)
        {
            if (PlayerPrefs.HasKey(name))
            {
                SetValue(PlayerPrefs.GetString(name), false);
            }
            else SetValue(startValue);
        }
    }

    public virtual void Save(string prefix)
    {
        if (canSave)
            PlayerPrefs.SetString(prefix + name, ToString());
    }

    public virtual void Load(string prefix)
    {
        if (canSave)
        {
            if (PlayerPrefs.HasKey(prefix + name))
            {
                SetValue(PlayerPrefs.GetString(prefix + name), false);
            }
            else SetValue(startValue);
        }
    }
}
