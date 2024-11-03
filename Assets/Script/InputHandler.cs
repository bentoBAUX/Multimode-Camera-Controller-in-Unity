using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance { get; private set; }
    public static bool IsRTSCameraMode { get; private set; } = false;
    public static bool IsFPSCameraMode { get; private set; } = false;

    [Header("Input Actions")]
    public InputActionReference movementAction;
    public InputActionReference jumpAction;
    public InputActionReference panAction;
    public InputActionReference zoomAction;
    public InputActionReference switchCameraAction;
    public InputActionReference switchToRTSAction;

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
        panAction.action.Enable();
        zoomAction.action.Enable();
        switchCameraAction.action.Enable();
        switchToRTSAction.action.Enable();
    }

    private void OnDisable()
    {
        movementAction.action.Disable();
        jumpAction.action.Disable();
        panAction.action.Disable();
        zoomAction.action.Disable();
        switchCameraAction.action.Disable();
        switchToRTSAction.action.Disable();
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

    public Vector2 GetPanInput()
    {
        return panAction.action.ReadValue<Vector2>();
    }

    public float GetZoomInput()
    {
        return zoomAction.action.ReadValue<float>();
    }

    public bool IsSwitchingToRTS()
    {
        return switchToRTSAction.action.triggered;
    }

    public void SetRTSCameraMode(bool isRTS)
    {
        Debug.Log("RTS: " + isRTS);
        IsRTSCameraMode = isRTS;
    }

    public void SetFPSCameraMode(bool isFPS)
    {
        Debug.Log("FPS: " + isFPS);
        IsFPSCameraMode = isFPS;
    }
}