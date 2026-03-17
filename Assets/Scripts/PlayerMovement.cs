using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private InputManager inputManager;
    public float speed = 2f;
    public float rotationSpeed = 120f;
    public Vector2 moveInput;
    bool rotateLeft;
    bool rotateRight;
    public float jumpHeight = 1f;
    public float gravity = -9.81f;
    private float yVelocity;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;

        inputManager.onMove.AddListener(SetMove);
        inputManager.onRotateLeftStart.AddListener(() => rotateLeft = true);
        inputManager.onRotateLeftStop.AddListener(() => rotateLeft = false);

        inputManager.onRotateRightStart.AddListener(() => rotateRight = true);
        inputManager.onRotateRightStop.AddListener(() => rotateRight = false);
        inputManager.onJump.AddListener(Jump);
    }

    void SetMove(Vector2 move)
    {
        moveInput = move;
    }

    void Jump()
    {
        if (characterController.isGrounded)
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (characterController.isGrounded && yVelocity < 0)
        {
            yVelocity = -2f;
        }

        yVelocity += gravity * Time.deltaTime;

        move.y = yVelocity;
        characterController.Move(move * speed * Time.deltaTime);

        if (rotateLeft)
            transform.Rotate(Vector3.up * -rotationSpeed * Time.deltaTime);

        if (rotateRight)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}