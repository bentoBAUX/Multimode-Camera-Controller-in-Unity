using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }

    [Header("Input Actions")]
    public InputActionReference movementAction;
    public InputActionReference jumpAction;
    public InputActionReference switchCameraAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        movementAction.action.Enable();
        jumpAction.action.Enable();
        switchCameraAction.action.Enable();
    }

    private void OnDisable()
    {
        movementAction.action.Disable();
        jumpAction.action.Disable();
        switchCameraAction.action.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return movementAction.action.ReadValue<Vector2>(); // Reads WASD or joystick input
    }

    public bool IsJumping()
    {
        return jumpAction.action.triggered; // Checks if the jump button was pressed
    }

    public bool IsSwitchingCamera()
    {
        return switchCameraAction.action.triggered; // Checks if camera switch was triggered
    }
}