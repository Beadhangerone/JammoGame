using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammoColider : MonoBehaviour
{

    private const string TrampolineTag = "Trampoline";
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag(TrampolineTag))
        {
            
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject other = hit.gameObject;
        if (other.CompareTag(TrampolineTag))
        {
            gameObject.GetComponent<MyMoveInput>().SuperJump();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
