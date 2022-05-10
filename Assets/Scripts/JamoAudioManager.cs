using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class JamoAudioManager : MonoBehaviour
{
    private AudioSource _commonSource;
    private AudioSource _stepSource;
    public AudioClip jump;
    public AudioClip step;
    private bool stepReady = true;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _commonSource = gameObject.AddComponent<AudioSource>();
        _stepSource = gameObject.AddComponent<AudioSource>();
        _stepSource.loop = false;
        _stepSource.clip = step;
        _stepSource.volume = 0.15f;
    }

    void FixedUpdate()
    {
    }
    
    public void PlayJump()
    {
        _commonSource.PlayOneShot(jump, 1f);
    }

    public void PlayStep()
    {
        if (!_stepSource.isPlaying)
        {
            _stepSource.pitch = .75f;
            // _stepSource.pitch *= -1;
            // if (_stepSource.pitch < 0)
            //     _stepSource.timeSamples = step.samples - 1;
            _stepSource.Play();
        }

    }

    public void PlayRun()
    {
        _stepSource.pitch = 1f;
        PlayStep();
    }
    
    
}
