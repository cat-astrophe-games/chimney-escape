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
        startingHeight.SetValue(transform.position.y - playerTracking.position.y);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Max(minimumHeight + startingHeight, playerTracking.position.y + startingHeight), transform.position.z);
    }
}
