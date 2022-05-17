using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopwatchOutput : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeText;

    private static Stopwatch _stopwatch;

    private void Awake()
    {
        _stopwatch = Stopwatch.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        _stopwatch.Begin();
        
    }

    // Update is called once per frame
    void Update()
    {
        _stopwatch.Update();
        
        if (_stopwatch._running)
        {
            timeText.text = _stopwatch.Time.GetText();    
        }
    }
}
