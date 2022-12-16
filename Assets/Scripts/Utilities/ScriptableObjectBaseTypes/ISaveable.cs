using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public interface ISaveable
{
    public bool CanSave { get; }
    public UnityEvent OnValueChangedNoData { get; }
    public void Save();

    public void Load();

    public void Save(string prefix);
    public void Load(string prefix);
}
