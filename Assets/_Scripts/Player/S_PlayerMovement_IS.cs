using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement_IS : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.5f;

    [SerializeField] private bool canDash;
    [SerializeField] private bool reverseControlsUpDown;
    [SerializeField] private bool reverseControlsLeftRight;

    [SerializeField] private Animator foxAnimator;
    
    private float _movementX;
    private float _movementY;
    private float _ySpeed;

    private bool _hasDashed;
    private bool _isDashing;
    private float _dashX;
    private float _dashY;

    private Vector2 _rotationVector;
    
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    public void DashAllowed(bool value)
    {
        canDash = value;
    }
    
    public void ReverseControlsUpDown(bool value)
    {
        reverseControlsUpDown = value;
    }
    
    public void ReverseControlsLeftRight(bool value)
    {
        reverseControlsLeftRight = value;
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        if (reverseControlsUpDown)
        {
            input.y *= -1;
        }
        if (reverseControlsLeftRight)
        {
            input.x *= -1;
        }
        if (_isDashing)
        {
            _dashX = input.x;
            _dashY = input.y;
            return;
        }
        _movementX = input.x;
        _movementY = input.y;

        if (_movementX != 0 || _movementY != 0)
        {
            transform.rotation = Quaternion.Euler(0, GetRotation(), 0);
        }
        else
        {
            foxAnimator.SetBool(IsWalking, false);
        }
    }
    
    private void OnJump()
    {
        if (_controller.isGrounded)
        {
            _ySpeed = jumpForce;
            foxAnimator.SetBool(IsWalking, false);
        }
        else if (canDash && !_isDashing && !_hasDashed)
        {
            StartCoroutine(Dash());
            _dashX = _movementX;
            _dashY = _movementY;
            _movementX = 0;
            _movementY = 0;
        }
    }
    
    private void Update()
    {
        if (!_controller.isGrounded && !_isDashing)
        {
            _ySpeed -= gravity * Time.deltaTime;
        }

        if (!_isDashing)
        {
            _moveDirection = new Vector3(_movementX, _ySpeed, _movementY);
            _moveDirection *= speed;
            if (_movementX != 0 || _movementY != 0)
            {
                foxAnimator.SetBool(IsWalking, _controller.isGrounded);
            }
        }
        else
        {
            _moveDirection = transform.forward;
            _moveDirection *= dashForce;
        }
        
        _controller.Move(_moveDirection * Time.deltaTime);
        
        if (_controller.isGrounded && !Mathf.Approximately(_ySpeed, -1))
        {
            _ySpeed = -1f;
        }

        if (_controller.isGrounded && _hasDashed)
        {
            _hasDashed = false;
        }
    }
    
    IEnumerator Dash()
    {
        _isDashing = true;
        _hasDashed = true;
        _ySpeed = 0;
        yield return new WaitForSeconds(dashDuration);
        _isDashing = false;
        _movementX = _dashX;
        _movementY = _dashY;
        if (_movementX != 0 || _movementY != 0)
        {
            transform.rotation = Quaternion.Euler(0, GetRotation(), 0);
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
