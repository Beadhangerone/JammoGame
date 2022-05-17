using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public GameObject player;
    private MyMoveInput _playerMove;
    private Rigidbody _rigidbody;
    
    // Start is called before the first frame update
    void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _playerMove = player.GetComponent<MyMoveInput>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        
        if (other == player)
        {
            _playerMove.SuperJump();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject other = collision.gameObject;
        
        if (other == player)
        {
            _playerMove.SuperJump(false);
        }
    }
}
