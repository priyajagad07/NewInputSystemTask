using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private SessionInput sessionInput;

    public UnityEvent<Vector2> onMove = new UnityEvent<Vector2>();
    public UnityEvent onRotateLeftStart = new UnityEvent();
    public UnityEvent onRotateLeftStop = new UnityEvent();
    public UnityEvent onRotateRightStart = new UnityEvent();
    public UnityEvent onRotateRightStop = new UnityEvent();
    public UnityEvent onJump = new UnityEvent();

    public UnityEvent onMousePress = new UnityEvent();
    public UnityEvent onMouseRelease = new UnityEvent();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        sessionInput = new SessionInput();
    }

    void OnEnable()
    {
        sessionInput.Enable();

        sessionInput.Player.Move.performed += move =>
        {
            onMove.Invoke(move.ReadValue<Vector2>());
        };

        sessionInput.Player.Move.canceled += move =>
        {
            onMove.Invoke(Vector2.zero);
        };

        sessionInput.Player.RotateLeft.started += rotateLeft =>
        {
            onRotateLeftStart.Invoke();
        };

        sessionInput.Player.RotateLeft.canceled += rotateLeft =>
        {
            onRotateLeftStop.Invoke();
        };

        sessionInput.Player.RotateRight.started += rotateRight =>
        {
            onRotateRightStart.Invoke();
        };

        sessionInput.Player.RotateRight.canceled += rotateRight =>
        {
            onRotateRightStop.Invoke();
        };

        sessionInput.Player.Jump.started += jump =>
        {
            onJump.Invoke();
        };
    }

    void OnDisable()
    {
        sessionInput.Disable();
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            onMousePress.Invoke();

        if (Mouse.current.leftButton.wasReleasedThisFrame)
            onMouseRelease.Invoke();
    }
}