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
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
}