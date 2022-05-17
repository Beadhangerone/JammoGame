using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    private MyLevelManager _levelManager;
    public TMPro.TextMeshProUGUI level1Best;
    public TMPro.TextMeshProUGUI level2Best;
    public TMPro.TextMeshProUGUI level3Best;

    private void Awake()
    {
        _levelManager = MyLevelManager.Instance; 
        bool passed1 = _levelManager.GetBestForLevel(1) != null;
        if (passed1)
        {
            level1Best.text = "PASSED";
        }
        bool passed2 = _levelManager.GetBestForLevel(2) != null;
        if (passed2)
        {
            level2Best.text = "PASSED";
        }
        bool passed3 = _levelManager.GetBestForLevel(3) != null;
        if (passed3)
        {
            level3Best.text = "PASSED";
        }
           
    }

    public void Level1BtnClick()
    {
        _levelManager.LoadLevel(1);
    }
    
    public void Level2BtnClick()
    {
        _levelManager.LoadLevel(2);
    }
    
    public void Level3BtnClick()
    {
        _levelManager.LoadLevel(3);
    }
}
