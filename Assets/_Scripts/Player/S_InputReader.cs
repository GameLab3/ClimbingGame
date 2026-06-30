using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputReader : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    // Player Game Actions
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _pauseAction;

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
    }
}
