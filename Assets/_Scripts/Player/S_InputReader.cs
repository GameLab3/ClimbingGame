using System;
using PinePie.SimpleJoystick;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private JoystickController joystickController;
    
    private Vector2 _movement;
    
    // Player Game Actions
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _pauseAction;
    private InputAction _touchOnAction;
    private InputAction _touchOffAction;
    
    // Player Events
    public static event Action TouchedScreen;
    public static event Action KeyboardPressed;
    public static event Action Jumped;
    public static event Action<Vector2> Movement;

    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }
    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();
    }
    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Player/Move");
        _jumpAction = InputSystem.actions.FindAction("Player/Jump");
        _pauseAction = InputSystem.actions.FindAction("Player/Pause");
        _touchOnAction = InputSystem.actions.FindAction("Player/TouchScreenOn");
        _touchOffAction = InputSystem.actions.FindAction("Player/TouchScreenOff");
    }

    private void Update()
    {
        Movement?.Invoke(joystickController.InputDirection == Vector2.zero
            ? _moveAction.ReadValue<Vector2>()
            : joystickController.InputDirection);

        if (_jumpAction.WasPressedThisFrame())
        {
            Jumped?.Invoke();
        }

        if (_touchOnAction.WasPressedThisFrame())
        {
            TouchedScreen?.Invoke();
        }

        if (_touchOffAction.WasPressedThisFrame())
        {
            KeyboardPressed?.Invoke();
        }
    }

    #region Public Custom Actions

    public void OnJumpAction()
    {
        Jumped?.Invoke();
    }

    #endregion
}
