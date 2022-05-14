using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyLevelManager : MonoBehaviour
{
    private SaveManager _saveManager;
    public GameObject stopwatchManager;

    private void Start()
    {
        _saveManager = SaveManager.Instance;
    }

    public static void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void FinishLevel1()
    {
        Stopwatch stopwatch = stopwatchManager.GetComponent<Stopwatch>();
        stopwatch.Pause();
        _saveManager.PlayerSave.SaveResultForLevel(1, stopwatch.Time);
        SceneManager.LoadScene("MainMenu");
    }
}
