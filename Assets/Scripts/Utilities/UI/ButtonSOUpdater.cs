using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSOUpdater : MonoBehaviour
{
    [SerializeField] IntValue value;
    [SerializeField] int change;

    [SerializeField] Button button;

    public void OnClick()
    {
        value.SetValue(value + change);
    }

    private void OnValidate()
    {
        if (!button) button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(OnClick);
    }
}
