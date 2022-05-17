using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private MyLevelManager _levelManager;
    public TMPro.TextMeshProUGUI level1Best;

    private void Awake()
    {
        _levelManager = MyLevelManager.Instance;
        level1Best.text = _levelManager.GetBestForLevel(1).GetText();
    }

    public void Level1BtnClick()
    {
        _levelManager.LoadLevel1();
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
