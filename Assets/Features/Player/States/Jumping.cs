public class Jumping : State
{
    public Jumping(Controller controller, MovementModel movementModel) : base(controller, movementModel) { }
    public override void OnEnter()
    {
        movementModel.Jump();
        movementModel.ApplyAirDrag();
    }
}