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
    private float _verticalVelocity;
    private float _inputX, _inputY;

    private float gravity = -9.8f;
    private float groundedGravity = -.5f;
    
    private Vector3 _desiredMoveDirection;

    private bool _isRunning = false;
    private bool _isJumping = false;

    public float desiredVelocity = 10;
    public float desiredRotationSpeed = 0.1f;

    [Range(0, 1f)] public float startAnimDamp = 0.3f;

    void handleGravity()
    {
        if (_characterController.isGrounded)
        {
            
        }
    }

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
        _horizontalVelocity = new Vector2(_inputX, _inputY).sqrMagnitude;

        var forward = _mainCameraTransform.forward;
        var right = _mainCameraTransform.right;
        var up = _mainCameraTransform.up;
            
        // moving
        forward.y = 0f;
        right.y = 0f;
        //falling
        up.x = 0f;
        up.z = 0f;
        up.y = _verticalVelocity;

        if (_horizontalVelocity == 0)
        {
            _animator.SetFloat (PlayerVelocity, 0.0f, 0, Time.deltaTime);
        }
        else if (_horizontalVelocity > 0.1 && _verticalVelocity == 0)
        {
            forward.Normalize();
            right.Normalize();
            up.Normalize();
            
            _desiredMoveDirection = forward * _inputY + right * _inputX;
            _animator.SetFloat(PlayerVelocity, _horizontalVelocity, startAnimDamp, Time.deltaTime);
            _characterController.Move(_desiredMoveDirection.normalized * (Time.deltaTime * desiredVelocity));
        }else if (_verticalVelocity != 0)
        {
            _animator.SetFloat (PlayerVerticalVelocity, _verticalVelocity, startAnimDamp, Time.deltaTime);
        }


        if (_desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (_desiredMoveDirection), desiredRotationSpeed);
        }
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
        Debug.Log(_isRunning);
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
