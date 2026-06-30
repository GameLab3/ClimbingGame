using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_PlayerMovement_IS : MonoBehaviour
{
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    
    [Header("Movement Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = 20f;

    [Header("Dash Settings")]
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private bool canDash;
    
    [Header("Control Options")]
    [SerializeField] private bool reverseAllControls;
    [SerializeField] private bool reverseControlsUpDown;
    [SerializeField] private bool reverseControlsLeftRight;
    [SerializeField] private bool usingControlStick;
    
    [Header("References")]
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Animator foxAnimator;
    
    // Movement variables
    // private float _movementX;
    // private float _movementY;
    private Vector2 _movement;
    private float _ySpeed;

    // Dash variables
    private bool _hasDashed;
    private bool _isDashing;
    private float _dashX;
    private float _dashY;
    
    private Vector2 _rotationVector;
    
    private Vector3 _moveDirection = Vector3.zero;
    private CharacterController _controller;
    private S_CheckPoint_IS _checkPoint;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    /// <summary>
    /// Update currently active checkpoint
    /// </summary>
    /// <param name="checkPoint">Currently active checkpoint</param>
    public void SetCheckPoint(S_CheckPoint_IS checkPoint)
    {
        _checkPoint = checkPoint;
    }
    
    /// <summary>
    /// Set Dash allowance state
    /// </summary>
    /// <param name="value">On/Off</param>
    public void DashAllowed(bool value)
    {
        canDash = value;
    }
    
    public void ReverseAllControls(bool value)
    {
        reverseAllControls = value;
    }
    
    public void ReverseControlsUpDown(bool value)
    {
        reverseControlsUpDown = value;
    }
    
    public void ReverseControlsLeftRight(bool value)
    {
        reverseControlsLeftRight = value;
    }
    
    public void Respawn()
    {
        _controller.enabled = false;
        if (_checkPoint != null)
        {
            transform.position = _checkPoint.transform.position;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        _controller.enabled = true;
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        if (reverseAllControls)
        {
            input *= -1;
        }
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
        //_movementX = input.x;
        //_movementY = input.y;
        _movement = new Vector2(input.x, input.y);

        if (usingControlStick)
        {
            if (reverseAllControls)
            {
                _movement *= -1;
            }
            _movement = GetRotationalMovement(_movement);
        }
        

        if (_movement.x != 0 || _movement.y != 0)
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
            _dashX = _movement.x;
            _dashY = _movement.y;
            _movement = Vector2.zero;
            /*_dashX = _movementX;
            _dashY = _movementY;
            _movementX = 0;
            _movementY = 0;*/
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
            _moveDirection = new Vector3(_movement.x, _ySpeed, _movement.y);
            
            
            // Rotates movement to be correct orientation
            //_moveDirection = Quaternion.AngleAxis(playerCamera.transform.eulerAngles.y, Vector3.up) * _moveDirection;
            
            
            _moveDirection *= speed;
            if (_movement.x != 0 || _movement.y != 0)
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
    
    private IEnumerator Dash()
    {
        _isDashing = true;
        _hasDashed = true;
        _ySpeed = 0;
        yield return new WaitForSeconds(dashDuration);
        _isDashing = false;
        _movement.x = _dashX;
        _movement.y = _dashY;
        if (_movement.x != 0 || _movement.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, GetRotation(), 0);
        }
    }

    private int GetRotation()
    {
        _rotationVector.x = _movement.x;
        _rotationVector.y = _movement.y;
        _rotationVector.Normalize();

        if (_rotationVector == Vector2.zero) return 0;
        float angle = Mathf.Atan2(_rotationVector.x, _rotationVector.y) * Mathf.Rad2Deg;
        return (int) angle;
    }

    private Vector2 GetRotationalMovement(Vector2 movement)
    {
        var movementVector = new Vector3(movement.x, 0, movement.y);
        var angle = Quaternion.AngleAxis(playerCamera.transform.eulerAngles.y, Vector3.up) * movementVector;
        return new Vector2(angle.x, angle.z);
    }
}
