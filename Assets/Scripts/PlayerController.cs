using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float maxHorizontalVelocity, minJumpVelocity, horizontalForce;
    
    [SerializeField] 
    private Rigidbody rb;

    [ReadOnly]
    public bool isOnGround;

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    public void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        horizontal = horizontal > 0 ? 1 : horizontal < 0 ? -1 : 0;
        if(horizontal != 0)
        {
            var multi = Mathf.Sign(horizontal) != Mathf.Sign(rb.velocity.x) ? 2 : 1;
            rb.AddForce(horizontalForce * horizontal * Time.fixedDeltaTime * multi, 0, 0);
        }
        else if (isOnGround)
        {
            var xSign = rb.velocity.x < 0 ? -1 : 1;
            rb.AddForce(-xSign * horizontalForce * Time.fixedDeltaTime * Mathf.Abs(rb.velocity.x), 0, 0);
        }
        if(vertical > 0 && isOnGround)
        {
            var velocity = rb.velocity;
            velocity.y = Mathf.Max(velocity.x, minJumpVelocity);
            rb.velocity = velocity;
        }
        var velocityConstraint = rb.velocity;
        velocityConstraint.x = Mathf.Min(velocityConstraint.x, maxHorizontalVelocity);
        rb.velocity = velocityConstraint;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = collision.collider.transform.position.y < transform.position.y;
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            Debug.Log($"Swapping X velocity");
            var velocity = rb.velocity;
            velocity.x = -velocity.x;
            rb.velocity = velocity;
            return;
        }
    }
}
