using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager
{
    private static SaveManager _saveManager;
    private static Stopwatch _stopwatch;    

    private static MyLevelManager _instance;

    public static MyLevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MyLevelManager();
            }

            return _instance;

        }
    }
    
    private MyLevelManager()
    {
        _stopwatch = Stopwatch.Instance;
        _saveManager = SaveManager.Instance;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void FinishLevel(int level)
    {
        _stopwatch.Pause();
        _saveManager.PlayerSave.SaveResultForLevel(level, _stopwatch.Time);
        SceneManager.LoadScene("MainMenu");
    }

    public MyTime GetBestForLevel(int level)
    {
        return _saveManager.PlayerSave.GetBestTimeForLevel(level);
    }

    public void FailLevel1()
    {
        _stopwatch.Reset();
        SceneManager.LoadScene("Level1");
    }
}