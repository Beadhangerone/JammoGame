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

    private float _currentVelocity;
    private float _inputX, _inputY;
    private Vector3 _desiredMoveDirection;

    private bool _isRunning = false;

    public float desiredVelocity = 10;
    public float desiredRotationSpeed = 0.1f; 
    
    [Range(0,1f)]
    public float startAnimDamp = 0.3f;

    private PlayerInput _playerInput;
    private InputAction _playerInputAction;

    //---other---
    private static readonly int PlayerVelocity = Animator.StringToHash("playerVelocity");

    void Awake()
    {
        _playerInput = gameObject.GetComponent<PlayerInput>();
        _playerInputAction = _playerInput.actions["Run"];

        _playerInputAction.started += context =>
        {
            _isRunning = context.ReadValueAsButton();
        };
        
        _playerInputAction.canceled += context =>
        {
            _isRunning = context.ReadValueAsButton();
            _inputX /= 2;
        };

    }
    //
    // private void OnEnable()
    // {
    //     _playerInputAction.Enable();
    // }
    //
    // private void OnDisable()
    // {
    //     _playerInputAction.Disable();
    // }


    // ---START---
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator> ();
        _mainCameraTransform = mainCamera.transform;
    }

    
    void Update()
    {
        if (_isRunning && _inputY + _inputX > 0 )
        {
            _inputY = 1;
            _inputY = 1;
        }
        _currentVelocity = new Vector2(_inputX, _inputY).sqrMagnitude;
        
        
        if (_currentVelocity < 0.1)
        {
            _currentVelocity = 0;
            _animator.SetFloat (PlayerVelocity, 0.0f, 0, Time.deltaTime);
        }
        else if (_currentVelocity > 0.1)
        {
            var forward = _mainCameraTransform.forward;
            var right = _mainCameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            _desiredMoveDirection = forward * _inputY + right * _inputX;
            _animator.SetFloat(PlayerVelocity, _currentVelocity, startAnimDamp, Time.deltaTime);
            _characterController.Move(_desiredMoveDirection.normalized * (Time.deltaTime * desiredVelocity));
        }


        if (_desiredMoveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (_desiredMoveDirection), desiredRotationSpeed);
        }
    }
    
    private void OnMove(InputValue movement)
    {
        Vector2 movementVect = movement.Get<Vector2>().normalized;

        _inputX = movementVect.x / 2;
        _inputY = movementVect.y / 2;

    }
    

}
