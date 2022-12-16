using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDecorator : MonoBehaviour
{
    [SerializeField] private TextMesh text;
    [SerializeField] private new MeshRenderer renderer;

    public void SetLabel(string text)
    {
        this.text.text = text;
    }

    public void SetMaterial(Material m)
    {
        var array = new Material[] { m };
        renderer.materials = array;
    }
}
