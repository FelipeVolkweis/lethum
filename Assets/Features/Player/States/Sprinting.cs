public class Sprinting : State
{
    public Sprinting(Controller controller, MovementModel movementModel) : base(controller, movementModel) { }
    public override void FixedUpdate()
    {
        movementModel.TurnTo(controller.GetMovementDirection(), movementModel.groundTurningSpeed);
        movementModel.MoveTo(controller.GetMovementDirection(), movementModel.sprintingSpeed);
    }
}