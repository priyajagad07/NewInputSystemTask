using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public SessionInput sessionInput;

    private void OnEnable()
    {
        sessionInput.Enable();

        sessionInput.Player.Move.performed += move => moveInput.move.ReadValue<Vector2>();
    }
}
