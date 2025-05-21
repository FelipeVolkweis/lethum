public interface IState
{
    void OnEnter();
    void OnExit();
    void Update();
    void FixedUpdate();
}
public abstract class State : IState
{
    public Controller controller;
    public MovementModel movementModel;
    public State(Controller controller, MovementModel movementModel)
    {
        this.controller = controller;
        this.movementModel = movementModel;
    }
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}