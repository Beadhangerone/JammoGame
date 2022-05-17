using System;
using UnityEngine;

public class Stopwatch
{
    private static Stopwatch _stopwatch;

    public static Stopwatch Instance
    {
        get
        {
            if (_stopwatch == null)
            {
                _stopwatch = new Stopwatch();
            }

            return _stopwatch;
        }
    }
    
    private float _runningStartTime = 0f;
    private float _pauseStartTime = 0f;
    private float _elapsedPausedTime = 0f;
    private float _totalElapsedPausedTime = 1f;
    public bool _running = false;
    public bool _paused = false;
    public MyTime Time { get; private set; }

    private Stopwatch()
    {
        Time = new MyTime();
    }

    public void Update()
    {
        if (_running)
        {
            Time.LapsedTime = UnityEngine.Time.time - _runningStartTime - _totalElapsedPausedTime;
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