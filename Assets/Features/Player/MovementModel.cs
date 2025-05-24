using UnityEngine;

public class MovementModel : MonoBehaviour
{
    [Header("Movement Settings")]
    public float runningSpeed;
    public float sprintingSpeed;
    public float airMoveSpeed;
    public float acceleration;
    public float groundTurningSpeed;
    public float airTurningSpeed;
    public float groundDrag;
    public float airDrag;
    public float jumpForce;
    public float fallMultiplier;

    [Header("References")]
    public Rigidbody body;
    public void Reset()
    {
        runningSpeed = 15f;
        sprintingSpeed = 25f;
        airMoveSpeed = 10f;
        acceleration = 50f;
        groundTurningSpeed = 8f;
        airTurningSpeed = 4f;
        groundDrag = 4f;
        airDrag = 0.5f;
        jumpForce = 10f;
        fallMultiplier = 2.5f;
        body = GetComponent<Rigidbody>();
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
    public void TurnTo(Vector3 direction, float turningSpeed)
    {
        if (direction == Vector3.zero) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * turningSpeed);
    }
    public void Jump()
    {
        body.linearVelocity = new Vector3(body.linearVelocity.x, 0f, body.linearVelocity.z);
        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    public void Fall()
    {
        body.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
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