using UnityEngine;
public class Controller : MonoBehaviour {

    [Header("MOVEMENT")]
    private float moveSpeed;
    public float walkSpeed = 5f;
    public float sprintSpeed = 8f;

    public float groundDrag = 4f;

    public float jumpForce = 10f;
    public float jumpCooldown = 0.25f;
    public float airMultiplier = 0.5f;
    bool readyToJump;

    [Header("Ground Check")]
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    Vector2 movementInput;
    Vector3 moveDirection;
    Rigidbody rb;
    public MovementState state;

    private PlayerControls inputActions;

    private bool jumpPressed;
    private bool sprintHeld;

    public enum MovementState {
        walking,
        sprinting,
        air
    }

    private void Awake() {
        inputActions = new PlayerControls();

        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
        inputActions.Player.Sprint.performed += ctx => sprintHeld = true;
        inputActions.Player.Sprint.canceled += ctx => sprintHeld = false;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update() {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        HandleMovementState();
        SpeedControl();

        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        HandleJump();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void HandleJump() {
        if (jumpPressed && readyToJump && grounded) {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        jumpPressed = false; // consume jump press
    }

    private void HandleMovementState() {
        if (grounded && sprintHeld) {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        else if (grounded) {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        else {
            state = MovementState.air;
        }
    }

    private void MovePlayer() {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump() {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }
}
