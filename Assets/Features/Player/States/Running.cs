public class Running : State
{
    public Running(Controller controller, MovementModel movementModel) : base(controller, movementModel) { }
    public override void FixedUpdate()
    { 
        movementModel.MoveTo(controller.GetMovementDirection(), movementModel.moveSpeed);
    }
}