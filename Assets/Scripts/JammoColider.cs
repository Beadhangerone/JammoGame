using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JammoColider : MonoBehaviour
{
    public GameObject levelManager;
    
    private const string TrampolineTag = "Trampoline";
    private const string FinishTag = "FinishArch";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        GameObject other = hit.gameObject;
        if (other.CompareTag(TrampolineTag))
        {
            gameObject.GetComponent<MyMoveInput>().SuperJump();
        }

        if (other.CompareTag(FinishTag))
        {
            levelManager.GetComponent<MyLevelManager>().FinishLevel1();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
