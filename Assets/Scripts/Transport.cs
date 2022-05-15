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

    private Func<Vector3, bool> _finalPositionReached;
    private Func<Vector3, bool> _initialPositionReached;
    // Start is called before the first frame update
    void Start()
    {
        _initialPosition = gameObject.transform.position;
        if (movementAxis == 'x')
        {
            _finalPosition = new Vector3(_initialPosition.x + movementDistance, _initialPosition.y, _initialPosition.z);   
            _forthMovementVector = new Vector3(1, 0, 0);
            _backMovementVector = new Vector3(-1, 0, 0);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.x >= _finalPosition.x;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.x <= _initialPosition.x;
        }else if (movementAxis == 'y')
        {
            _finalPosition = new Vector3(_initialPosition.x, _initialPosition.y + movementDistance, _initialPosition.z);
            _forthMovementVector = new Vector3(0, 1, 0);
            _backMovementVector = new Vector3(0, -1, 0);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.y >= _finalPosition.x;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.y <= _initialPosition.y;

        }
        else
        {
            _finalPosition = new Vector3(_initialPosition.x, _initialPosition.y, _initialPosition.z + movementDistance);
            _forthMovementVector = new Vector3(0, 0, 1);
            _backMovementVector = new Vector3(0, 0, -1);
            _finalPositionReached = (Vector3 currentPosition) => currentPosition.z >= _finalPosition.x;
            _initialPositionReached = (Vector3 currentPosition) => currentPosition.z <= _initialPosition.z;

        }
        
        //Debug.Log(_initialPosition +" -> "+ _finalPosition);
        
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _currentMovementVector = _forthMovementVector;
        Debug.Log(_initialPosition+"->"+_finalPosition);
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;

        Debug.Log(currentPosition+": "+_initialPositionReached(currentPosition)+"->"+_finalPositionReached(currentPosition));
        
        if (_finalPositionReached(currentPosition))
        {
            _currentMovementVector = _backMovementVector;
        }
        else if(_initialPositionReached(currentPosition))
        {
            _currentMovementVector = _forthMovementVector;
        }
        
        _rigidbody.AddForce(_currentMovementVector);
        
    }
    
    
}
