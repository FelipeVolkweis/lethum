using UnityEngine;
using Unity.Cinemachine;
public class Controller : MonoBehaviour
{
    public Transform orientation;
    Vector2 movementInput;
    Vector3 moveDirection;
    private PlayerControls inputActions;
    public CinemachineCamera cinemachine;
    private bool jumpPressed;
    private bool sprintHeld;
    private void Awake()
    {
        inputActions = new PlayerControls();

        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
        inputActions.Player.Sprint.performed += ctx => sprintHeld = true;
        inputActions.Player.Sprint.canceled += ctx => sprintHeld = false;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cinemachine = FindFirstObjectByType<CinemachineCamera>();
    }
    public Vector3 GetMovementDirection()
    {
        moveDirection = orientation.forward * movementInput.y + orientation.right * movementInput.x;
        return moveDirection.normalized;
    }
}
