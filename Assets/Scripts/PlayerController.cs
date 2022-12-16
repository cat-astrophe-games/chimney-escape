using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float maxHorizontalVelocity, minJumpVelocity, acceleration, bounceBonus;
    
    [SerializeField] 
    private Rigidbody rb;

    [ReadOnly]
    public bool isOnGround, canJump = true;

    [SerializeField]
    [ReadOnly]
    private int maxPlatformReached;

    [ReadOnly]
    public bool controlEnabled = true;

    private void Start()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        if(isOnGround)
            maxPlatformReached = Mathf.Max(maxPlatformReached, Mathf.RoundToInt(transform.position.y / 3f));
        if (rb.velocity.y > 0)
            canJump = false;
    }

    public int GetMaxRoundedPlatformPosition() => maxPlatformReached;

    public int GetCurrentPlatformPosition()
    {
        return Mathf.RoundToInt(transform.position.y / 3f);
    }

    public void FixedUpdate()
    {
        if (!controlEnabled) return;
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        horizontal = horizontal > 0 ? 1 : horizontal < 0 ? -1 : 0;
        var velocity = rb.velocity;
        if(horizontal != 0)
        {
            var multi = Mathf.Sign(horizontal) != Mathf.Sign(velocity.x) ? 2 : 1;
            var change = acceleration * horizontal * Time.fixedDeltaTime * multi;
            if (!isOnGround)
                change *= .75f;
            velocity.x += change;
        }
        else if (isOnGround)
        {
            velocity.x *= 0.9f;
        }
        velocity.x = Mathf.Min(velocity.x, maxHorizontalVelocity);
        if(vertical > 0 && isOnGround && velocity.y <= 0 && canJump)
        {
            velocity.y = Mathf.Max(velocity.x, minJumpVelocity);
            isOnGround = false;
        }
        rb.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = canJump = collision.transform.position.y < transform.position.y;
        if (isOnGround)
            Debug.Log("Landed");
    }

    private void OnCollisionStay(Collision collision)
    {
        isOnGround = canJump = collision.transform.position.y < transform.position.y;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.position.y < transform.position.y)
            isOnGround = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            var velocity = rb.velocity;
            velocity.x = -velocity.x;
            velocity.x += Mathf.Sign(velocity.x) * bounceBonus;
            velocity.y += bounceBonus;
            rb.velocity = velocity;
            return;
        }
    }
}
