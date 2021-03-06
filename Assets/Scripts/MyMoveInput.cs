using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static JamoAudioManager;

public class MyMoveInput : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    public Camera mainCamera;
    private Transform _mainCameraTransform;

    private float _horizontalVelocity;
    private float _inputX, _inputY;

    private float gravity = -9f;
    private float groundedGravity = -.09f;

    private Vector3 _moveDirection;
    private Vector3 _fallDirection;
    private Vector3 _currentMovement;
    public Vector3 externalMovement = Vector3.zero;

    private bool _isRunning = false;
    private bool _isJumpPressed = false;
    private bool _isJumping = false;
    private bool _superJump = false;

    public float initialJumpVelocity;
    public float desiredVelocity = 10;
    public float desiredRotationSpeed = 0.05f;
    public float desiredFallFactor = .1f;

    [Range(0, 1f)] public float startAnimDamp = 0.3f;

    //---move---
    private PlayerInput _playerInput;
    private InputAction _playerInputActionMove;
    private InputAction _playerInputActionRun;
    private InputAction _playerInputActionJump;

    //---other---
    private static readonly int PlayerVelocity = Animator.StringToHash("playerVelocity");
    private static readonly int PlayerVerticalVelocity = Animator.StringToHash("playerVerticalVelocity");

    private JamoAudioManager _jamoAudioManager;
   

    private void Awake()
    {

        _animator = GetComponent<Animator>();
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInputActionRun = _playerInput.actions["Run"];
        _playerInputActionMove = _playerInput.actions["Move"];
        _playerInputActionJump = _playerInput.actions["Jump"];

        _playerInputActionMove.started += context => MyOnMove(context);
        _playerInputActionMove.performed += context => MyOnMove(context);
        _playerInputActionMove.canceled += context => MyOnMove(context);

        _playerInputActionRun.started += context => MyOnRun(context);
        _playerInputActionRun.canceled += context => MyOnRun(context);

        _playerInputActionJump.started += context => MyOnJump(context);
        _playerInputActionJump.canceled += context => MyOnJump(context);
    }

    void Start()
    {
        _jamoAudioManager = GetComponent<JamoAudioManager>();
        _characterController = GetComponent<CharacterController>();
        _mainCameraTransform = mainCamera.transform;
    }
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleGravity();
        HandleJump();
        
        if (_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (_moveDirection), desiredRotationSpeed);
        }
        _currentMovement = new Vector3(_moveDirection.x, _fallDirection.y, _moveDirection.z);
        
        _characterController.Move(externalMovement * Time.deltaTime + (_currentMovement.normalized * (Time.deltaTime * desiredVelocity)));
        
    }
    
    private void HandleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
        {
            _isJumping = true;
            _jamoAudioManager.PlayJump();
            if (_superJump)
            {
                _fallDirection.y = 10f;
                _superJump = false;
            }
            else
            {
                _fallDirection.y = 3.5f;
                _superJump = false;
            }
            
        }else if (!_isJumpPressed && _isJumping && _characterController.isGrounded)
        {
            _isJumping = false;
        }
    }

    public void SuperJump(bool val = true)
    {
        _superJump = val;
    }
    
    private void HandleGravity()
    {
        _fallDirection.x = 0;
        _fallDirection.z = 0;
        
        if (_characterController.isGrounded)
        {
            _fallDirection.y = groundedGravity;
        }
        else
        {
            _fallDirection.y += gravity * Time.deltaTime;
        }
        _animator.SetFloat (PlayerVerticalVelocity, _fallDirection.y, 0, Time.deltaTime);
        
    }

    private void HandleAnimation()
    {
        _horizontalVelocity = new Vector2(_inputX, _inputY).sqrMagnitude;
        if (_horizontalVelocity >= .1f) {
            _animator.SetFloat (PlayerVelocity, _horizontalVelocity, startAnimDamp, Time.deltaTime); 
        } else {
            _animator.SetFloat (PlayerVelocity, 0f, 0, Time.deltaTime);
        }
    }

    private void HandleMovement()
    {
        var forward = _mainCameraTransform.forward;
        var right = _mainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        
        forward.Normalize();
        right.Normalize();

        if ((_inputX != 0 || _inputY != 0) && _characterController.isGrounded)
        {
            if (_isRunning)
            {
                _jamoAudioManager.PlayRun();
            }
            else
            {   
                _jamoAudioManager.PlayStep();
            }
        }
        _moveDirection = forward * _inputY + right * _inputX;
    }
    private void MyOnMove(InputAction.CallbackContext context)
    {
        var movement = context.ReadValue<Vector2>().normalized;
        
        _inputX = movement.x / 2;
        _inputY = movement.y / 2;
    }

    private void MyOnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();
        if (_isRunning) {
            _inputY *= 2;
        } else {
            _inputY = 0.5f;
        }
    }

    private void MyOnJump(InputAction.CallbackContext context)
    {
        _isJumpPressed = context.ReadValueAsButton();
    }
}
