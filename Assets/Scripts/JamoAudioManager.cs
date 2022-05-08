using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class JamoAudioManager : MonoBehaviour
{
    private AudioSource _source;
    public AudioClip jump;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
    }

    public void PlayJump()
    {
        _source.PlayOneShot(jump, 1f);
    }
}
