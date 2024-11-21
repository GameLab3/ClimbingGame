using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement_IS : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float rotationSpeed = 1.0f;
    
    private float _movementX;
    private float _movementY;
    private float _ySpeed;
    
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        _movementX = input.x;
        _movementY = input.y;
    }
    
    private void OnJump()
    {
        if (_controller.isGrounded)
        {
            _ySpeed = jumpForce;
        }
    }
    
    private void Update()
    {
        if (!_controller.isGrounded)
        {
            _ySpeed -= gravity * Time.deltaTime;
        }
        _moveDirection = new Vector3(_movementX, _ySpeed, _movementY);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= speed;
        _controller.Move(_moveDirection * Time.deltaTime);
        
        if (_controller.isGrounded && !Mathf.Approximately(_ySpeed, -1))
        {
            _ySpeed = -1f;
        }
    }
}
