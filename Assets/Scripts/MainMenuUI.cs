using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private MyLevelManager _levelManager;
    private SaveManager _saveManager;
    public TMPro.TextMeshProUGUI level1Best;

    private void Start()
    {
        _levelManager = gameObject.GetComponent<MyLevelManager>();
        _saveManager = SaveManager.Instance;
        level1Best.text = _saveManager.PlayerSave.GetBestTimeForLevel(1).GetTime();
    }

    public void Level1BtnClick()
    {
        MyLevelManager.LoadLevel1();
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
