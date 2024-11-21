using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement_IS : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 20f;
    
    private float _movementX;
    private float _movementY;
    private float _ySpeed;

    private Vector2 _rotationVector;
    
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

        if (_movementX == 0 && _movementY == 0) return;
        transform.rotation = Quaternion.Euler(0, GetRotation(), 0);
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
        _moveDirection *= speed;
        _controller.Move(_moveDirection * Time.deltaTime);
        
        if (_controller.isGrounded && !Mathf.Approximately(_ySpeed, -1))
        {
            _ySpeed = -1f;
        }
    }

    private int GetRotation()
    {
        _rotationVector.x = _movementX;
        _rotationVector.y = _movementY;
        _rotationVector.Normalize();

        if (_rotationVector == Vector2.zero) return 0;
        float angle = Mathf.Atan2(_rotationVector.x, _rotationVector.y) * Mathf.Rad2Deg;
        return (int) angle;

    }
}
