using System;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    private float _elapsedRunningTime = 0f;
    private float _runningStartTime = 0f;
    private float _pauseStartTime = 0f;
    private float _elapsedPausedTime = 0f;
    private float _totalElapsedPausedTime = 0f;
    private bool _running = false;
    private bool _paused = false;

    public TMPro.TextMeshProUGUI time;

    private void Start()
    {
        Begin();
    }

    void Update()
    {
        if (_running)
        {
            _elapsedRunningTime = Time.time - _runningStartTime - _totalElapsedPausedTime;
            String mins = (GetMinutes() < 10 ? "0" : "") + GetMinutes();
            String secs = (GetSeconds() < 10 ? "0" : "") + GetSeconds();
            time.text = mins + ":" + secs;
        }
        else if (_paused)
        {
            _elapsedPausedTime = Time.time - _pauseStartTime;
        }
    }
  
    public void Begin()
    {
        if (!_running && !_paused)
        {
            _runningStartTime = Time.time;
            _running = true;
        }
    }
  
    public void Pause()
    {
        if (_running && !_paused)
        {
            _running = false;
            _pauseStartTime = Time.time;
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
        _elapsedRunningTime = 0f;
        _runningStartTime = 0f;
        _pauseStartTime = 0f;
        _elapsedPausedTime = 0f;
        _totalElapsedPausedTime = 0f;
        _running = false;
        _paused = false;
    }
  
    public int GetMinutes()
    {
        return (int)(_elapsedRunningTime / 60f);
    }
  
    public int GetSeconds()
    {
        return (int)(_elapsedRunningTime);
    }
  
    public float GetMilliseconds()
    {
        return (float)(_elapsedRunningTime - System.Math.Truncate(_elapsedRunningTime));
    }
 
    public float GetRawElapsedTime()
    {
        return _elapsedRunningTime;
    }
}