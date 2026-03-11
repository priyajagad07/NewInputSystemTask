using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private SessionInput sessionInput;
    private CharacterController characterController;
    public float speed = 0.5f;
    public float rotationSpeed = 120f;
    public Vector2 moveInput;

    public bool rotateLeft;
    public bool rotateRight;

    public void Awake()
    {
        characterController = GetComponent<CharacterController>();
        sessionInput = new SessionInput();

    }
    private void OnEnable()
    {
        sessionInput.Enable();

        sessionInput.Player.Move.performed += (move) =>
        {
            moveInput = move.ReadValue<Vector2>();
        };

        sessionInput.Player.Move.canceled += (move) =>
        {
            moveInput = Vector2.zero;
        };

        sessionInput.Player.RotateLeft.started += rleft => rotateLeft = true;
        sessionInput.Player.RotateLeft.canceled += rleft => rotateLeft = false;

        sessionInput.Player.RotateRight.started += rright => rotateRight = true;
        sessionInput.Player.RotateRight.canceled += rright => rotateRight = false;
        // sessionInput.Player.RotateLeft.performed += rotateLeft =>
        // {
        //     transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);
        // };

        // sessionInput.Player.RotateRight.performed += rotateRight =>
        // {
        //     transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // };
    }
    private void OnDisable()
    {
        sessionInput.Disable();
    }

    void Update()
    {
        //Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        //characterController.Move(move * speed * Time.deltaTime);
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(move * speed * Time.deltaTime, Space.Self);

        if (rotateLeft)
            transform.Rotate(Vector3.up *- rotationSpeed * Time.deltaTime);

        if (rotateRight)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
