public class Landing : State
{
    public Landing(Controller controller, MovementModel movementModel) : base(controller, movementModel) { }
    public override void OnExit()
    {
        movementModel.ResetDrag();
    }
}