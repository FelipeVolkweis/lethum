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

        At(idleState, runningState, new FuncPredicate(() => IsMoving()));
        At(runningState, idleState, new FuncPredicate(() => !IsMoving()));

        stateMachine.SetState(idleState);
    }
}