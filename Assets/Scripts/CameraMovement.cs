using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTracking;
    [SerializeField] private FloatValue minimumHeight;
    [SerializeField] private FloatValue startingHeight;

    private void Start()
    {
        startingHeight.SetValue(transform.localPosition.y - playerTracking.position.y);
        minimumHeight.SetValue(minimumHeight.StartValue);
    }

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Max(minimumHeight + startingHeight, playerTracking.position.y + startingHeight), transform.localPosition.z);
    }
}
