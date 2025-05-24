using System;
public class Falling : State
{
    private float maxSpeed;
    public Falling(Controller controller, MovementModel movementModel) : base(controller, movementModel)
    { 
        maxSpeed = movementModel.runningSpeed;
    }
    public override void OnEnter()
    {
        maxSpeed = movementModel.body.linearVelocity.magnitude;
    }
    public override void FixedUpdate()
    {
        float speed = Math.Max(maxSpeed, movementModel.airMoveSpeed);
        movementModel.TurnTo(controller.GetMovementDirection(), movementModel.airTurningSpeed);
        movementModel.MoveTo(controller.GetMovementDirection(), speed);
        movementModel.Fall();
    }
}