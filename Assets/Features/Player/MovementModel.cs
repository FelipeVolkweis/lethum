using UnityEngine;

public class MovementModel : MonoBehaviour
{
    public float moveSpeed;
    public float acceleration;
    public float groundDrag;
    public float airDrag;
    public float jumpForce;
    public Rigidbody body;
    public void Reset()
    {
        moveSpeed = 25f;
        acceleration = 50f;
        groundDrag = 4f;
        airDrag = 0.5f;
        jumpForce = 10f;
    }
    public void Start()
    {
        body.freezeRotation = true;
    }
    public void MoveTo(Vector3 direction, float speed)
    {
        Vector3 desiredVelocity = direction * speed;
        Vector3 velocityDelta = desiredVelocity - body.linearVelocity;
       
        Vector3 accel = Vector3.ClampMagnitude(velocityDelta, acceleration);

        body.AddForce(accel * body.mass, ForceMode.Force);
    }
    public void Jump()
    {
        body.linearVelocity = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void ApplyGroundDrag()
    {
        body.linearDamping = groundDrag;
    }
    public void ApplyAirDrag()
    {
        body.linearDamping = airDrag;
    }
    public void ResetDrag()
    {
        body.linearDamping = 0f;
    }
}