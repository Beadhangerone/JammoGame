using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public GameObject player;

    private CharacterController _characterController;

    void Awake()
    {
        _characterController = player.GetComponent<CharacterController>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("YAY");
        GameObject colidedWith = collision.gameObject;
        if (colidedWith == player)
        {
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
