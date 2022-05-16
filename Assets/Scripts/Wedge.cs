using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wedge : MonoBehaviour
{
    private float _angle;
    private float _rotation;
    private Vector3 _externalForce;

    public GameObject player;
    private MyMoveInput _moveInput;
    public float SlipFactor;
    
    private void Awake()
    {
        Rigidbody _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _moveInput = player.GetComponent<MyMoveInput>();
        
        float rotationX = transform.eulerAngles.x;
        float rotationY = transform.eulerAngles.y;

        _angle = rotationX - 360;
        _rotation = rotationY - 360;

        float wedge = Math.Abs(_angle) / 30;
        if (_angle < 0)
        {
            wedge *= -1;
        }
        
        Vector3 wedgeForce = new Vector3(0, 0, wedge);
        
        _externalForce = Quaternion.Euler(_angle, _rotation, 0) * wedgeForce * SlipFactor;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other == player)
        {
            _moveInput.externalMovement = _externalForce;
            
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other == player)
        {
            _moveInput.externalMovement = Vector3.zero;
        }
    }
}
