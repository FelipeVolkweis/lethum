using UnityEngine;
public class Player : MonoBehaviour
{
    public MovementModel MovementModel { get; private set; }
    public Controller Controller { get; private set; }
    public float playerHeight = 2f;
    public LayerMask groundLayerMask;
    public StateMachine stateMachine;
    public void Awake()
    {
        Controller = GetComponent<Controller>();
        MovementModel = GetComponent<MovementModel>();
        SetupStates();
    }
    public void Update()
    {
        stateMachine.Update();
    }
    public void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayerMask);
    }
    public bool IsMoving()
    {
        return Controller.GetMovementDirection().magnitude > 0f;
    }
    void At(IState from, IState to, IPredicate predicate) => stateMachine.AddTransition(from, to, predicate);
    void Any(IState to, IPredicate predicate) => stateMachine.AddAnyTransition(to, predicate);
    private void SetupStates()
    {
        stateMachine = new StateMachine();
        var idleState = new Idle(Controller, MovementModel);
        var runningState = new Running(Controller, MovementModel);
        var sprintingState = new Sprinting(Controller, MovementModel);

        var jumpingState = new Jumping(Controller, MovementModel);
        var fallingState = new Falling(Controller, MovementModel);
        var landingState = new Landing(Controller, MovementModel);

        At(idleState, runningState, new FuncPredicate(() => IsMoving()));
        At(idleState, sprintingState, new FuncPredicate(() => Controller.IsSprinting() && IsMoving()));
        At(runningState, idleState, new FuncPredicate(() => !IsMoving()));
        At(runningState, sprintingState, new FuncPredicate(() => Controller.IsSprinting() && IsMoving()));
        At(sprintingState, runningState, new FuncPredicate(() => !Controller.IsSprinting() && IsMoving()));
        At(sprintingState, idleState, new FuncPredicate(() => !Controller.IsSprinting() && !IsMoving()));

        Any(fallingState, new FuncPredicate(() => !IsGrounded()));
        At(jumpingState, fallingState, new FuncPredicate(() => !IsGrounded()));
        At(fallingState, landingState, new FuncPredicate(() => IsGrounded()));
        At(landingState, idleState, new FuncPredicate(() => IsGrounded()));
        
        At(idleState, jumpingState, new FuncPredicate(() => Controller.IsJumpPressed()));
        At(runningState, jumpingState, new FuncPredicate(() => Controller.IsJumpPressed()));
        At(sprintingState, jumpingState, new FuncPredicate(() => Controller.IsJumpPressed()));

        stateMachine.SetState(idleState);
    }
}