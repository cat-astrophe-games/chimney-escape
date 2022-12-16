using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneWayCollider : MonoBehaviour
{
    [SerializeField]
    private Collider toDisable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            toDisable.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            toDisable.enabled = true;
    }
}
