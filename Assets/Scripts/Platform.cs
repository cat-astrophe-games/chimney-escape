using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    [SerializeField] private GameObject labelHolder;
    [SerializeField] private Transform platform;

    public void SetLabel(string text)
    {
        labelHolder.SetActive(!string.IsNullOrEmpty(text));
        this.text.text = text;
    }

    public void DisableLabel()
    {
        labelHolder.SetActive(false);
    }

    public void SetLength(float x)
    {
        var scale = platform.localScale;
        scale.x = x;
        platform.localScale = scale;
    }
}
