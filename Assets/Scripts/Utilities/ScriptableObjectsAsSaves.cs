using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObjectsAsSaves : MonoBehaviour
{
    [SerializeReference] private List<ScriptableObject> SOs;

    private List<ISaveable> saveables;

    private void Start()
    {
        Init();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            PlayerPrefs.Save();
    }

    public void Init()
    {
        saveables = new List<ISaveable>();
        foreach (var so in SOs)
        {
            if (so is ISaveable)
            {
                saveables.Add(so as ISaveable);
                Debug.Log($"Added {so.name} as ISaveable");
            }
        }
        foreach (var value in saveables)
        {
            value.Load();
        }
    }

    public void CreateSave(string prefix)
    {
        foreach(var value in saveables)
        {
            value.Save(prefix);
        }
    }

    public void LoadSave(string prefix)
    {
        foreach(var value in saveables)
        {
            value.Load(prefix);
        }
    }
}
