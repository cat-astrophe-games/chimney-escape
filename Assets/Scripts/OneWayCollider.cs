using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class OneWayCollider : MonoBehaviour
{
    [SerializeField]
    private Collider toDisable;

    private const string PlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(PlayerTag))
            toDisable.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag))
            toDisable.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(PlayerTag))
        {
            if(other.attachedRigidbody.velocity.y <= 0 && other.transform.position.y > toDisable.transform.position.y)
                toDisable.enabled = true;
            else
                toDisable.enabled = false;
        }
    }
}
