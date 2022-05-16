using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    private Vector3 _initialPosition;
    private Vector3 _finalPosition;
    private Vector3 _forthMovementVector;
    private Vector3 _backMovementVector;
    private Vector3 _currentMovementVector;
    private Rigidbody _rigidbody;
    
    public int movementDistance = 10;
    public char movementAxis = 'z';
    public float movementSpeed;

    private Func<Vector3, bool> _finalPositionReached;
    private Func<Vector3, bool> _initialPositionReached;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = gameObject.transform.position;
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        
        if (movementAxis == 'x')
        {
            _finalPosition = new Vector3(_initialPosition.x + movementDistance, _initialPosition.y, _initialPosition.z);   
            _forthMovementVector = new Vector3(1, 0, 0);
            _backMovementVector = new Vector3(-1, 0, 0);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.x >= _finalPosition.x;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.x <= _initialPosition.x;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }else if (movementAxis == 'y')
        {
            _finalPosition = new Vector3(_initialPosition.x, _initialPosition.y + movementDistance, _initialPosition.z);
            _forthMovementVector = new Vector3(0, 1, 0);
            _backMovementVector = new Vector3(0, -1, 0);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.y >= _finalPosition.y;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.y <= _initialPosition.y;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            _finalPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z + movementDistance);
            _forthMovementVector = new Vector3(0, 0, movementSpeed);
            _backMovementVector = new Vector3(0, 0, -movementSpeed);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.z >= _finalPosition.z;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.z <= _initialPosition.z;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

        }
        
        _currentMovementVector = _forthMovementVector;
        Debug.Log(_initialPosition+"->"+_finalPosition);
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;
        
        if (_finalPositionReached(currentPosition))
        {
            _currentMovementVector = _backMovementVector;
        }
        else if(_initialPositionReached(currentPosition))
        {
            _currentMovementVector = _forthMovementVector;
        }
        Debug.Log(currentPosition+": "+_currentMovementVector);
        gameObject.transform.position = currentPosition + _currentMovementVector * Time.deltaTime;
        
    }
    
    
}
