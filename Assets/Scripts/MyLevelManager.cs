using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    private SaveManager _saveManager;
    public GameObject stopwatchManager;
    private Stopwatch stopwatch;    

    private void Start()
    {
        _saveManager = SaveManager.Instance;
        stopwatch = stopwatchManager.GetComponent<Stopwatch>();
    }

    public static void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void FinishLevel1()
    {
        stopwatch.Pause();
        _saveManager.PlayerSave.SaveResultForLevel(1, stopwatch.Time);
        SceneManager.LoadScene("MainMenu");
    }

    public void FailLevel1()
    {
        stopwatch.Reset();
        SceneManager.LoadScene("Level1");
    }
}