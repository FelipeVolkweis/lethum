using UnityEngine;
public class Player : MonoBehaviour
{
    public MovementModel MovementModel { get; }
    public float playerHeight = 2f;
    public LayerMask groundLayerMask;
    public Player()
    {
        MovementModel = new();
    }
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayerMask);
    }
}