public class Idle : State
{
    public Idle(Controller controller, MovementModel movementModel) : base(controller, movementModel) { }
    public override void OnEnter()
    {
        movementModel.ApplyGroundDrag();
    }
    public override void OnExit()
    {
        movementModel.ResetDrag();
    }
}