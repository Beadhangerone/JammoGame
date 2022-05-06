using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyMoveInput : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    public Camera mainCamera;
    private Transform _mainCameraTransform;

    private float _horizontalVelocity;
    private float _inputX, _inputY;

    private float gravity = -9.8f;
    private float groundedGravity = -.09f;

    private Vector3 _moveDirection;
    private Vector3 _fallDirection;
    private Vector3 _currentMovement;

    private bool _isRunning = false;
    private bool _isJumping = false;

    public float initialJumpVelocity;
    public float desiredVelocity = 10;
    public float desiredRotationSpeed = 0.1f;
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
        _characterController = GetComponent<CharacterController>();
        _mainCameraTransform = mainCamera.transform;
    }
    void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleGravity();
        
        if (_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (_moveDirection), desiredRotationSpeed);
        }
        _currentMovement = new Vector3(_moveDirection.x, _fallDirection.y, _moveDirection.z);
        _characterController.Move(_currentMovement.normalized * (Time.deltaTime * desiredVelocity));
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
        Debug.Log(_fallDirection.y);
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
        _isJumping = context.ReadValueAsButton();
    }
}
