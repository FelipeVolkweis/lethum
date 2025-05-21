using UnityEngine;

public class MovementModel : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float groundDrag = 4f;
    public float airDrag = 0.5f;
    public float jumpForce = 10f;
    public Rigidbody body;
    public void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }
    public void MoveTo(Vector3 direction, float speed)
    {
        Vector3 desiredVelocity = direction * moveSpeed;
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
    public void ClampSpeed()
    {
        Vector3 flatVel = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            body.linearVelocity = new Vector3(limitedVel.x, body.linearVelocity.y, limitedVel.z);
        }
    }
}