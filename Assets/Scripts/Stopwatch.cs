using System;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private static Stopwatch _instance;

    public static Stopwatch Instance
    {
        get { return _instance; }
    }
    
    private float _runningStartTime = 0f;
    private float _pauseStartTime = 0f;
    private float _elapsedPausedTime = 0f;
    private float _totalElapsedPausedTime = 1f;
    private bool _running = false;
    private bool _paused = false;
    public MyTime Time { get; private set; }
    
    public TMPro.TextMeshProUGUI timeText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
    
    private void Start()
    {
        Time = new MyTime();
        Begin();
    }

    void Update()
    {
        if (_running)
        {
            Time.LapsedTime = UnityEngine.Time.time - _runningStartTime - _totalElapsedPausedTime;

            timeText.text = Time.GetTime();
        }
        else if (_paused)
        {
            Time.LapsedTime = UnityEngine.Time.time - _pauseStartTime;
        }
    }
  
    public void Begin()
    {
        if (!_running && !_paused)
        {
            _runningStartTime = UnityEngine.Time.time;
            _running = true;
        }
    }
  
    public void Pause()
    {
        if (_running && !_paused)
        {
            _running = false;
            _pauseStartTime = UnityEngine.Time.time;
            _paused = true;
        }
    }
  
    public void Unpause()
    {
        if (!_running && _paused)
        {
            _totalElapsedPausedTime += _elapsedPausedTime;
            _running = true;
            _paused = false;
        }
    }
  
    public void Reset()
    {
        Time.LapsedTime = 0f;
        _runningStartTime = 0f;
        _pauseStartTime = 0f;
        _elapsedPausedTime = 0f;
        _totalElapsedPausedTime = 0f;
        _running = false;
        _paused = false;
    }
}