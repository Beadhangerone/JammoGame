using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    private MySceneManager _sceneManager;

    private void Start()
    {
        _sceneManager = gameObject.GetComponent<MySceneManager>();
    }

    public void Level1BtnClick()
    {
        _sceneManager.LoadLevel1();
        Debug.Log("level 1");
    }
    
    public void Level2BtnClick()
    {
        Debug.Log("level 2");
    }
    
    public void Level3BtnClick()
    {
        Debug.Log("level 3");
    }
}
