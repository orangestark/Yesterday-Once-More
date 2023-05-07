using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindSound : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    private float _timer;
    private float _againTimer;
    private int _index = 0;
    private float[] _timestamps = {0f, 2.28f, 2.52f, 2.52f, 2.73f, 2.93f, 3.23f, 3.38f};
    private float[] _restart = {3.23f, 3.38f, 3.38f, 3.53f, 3.53f, 3.8f, 3.8f, 4.4f};

    private void Start()
    {
        audioSource.clip = audioClip;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            _againTimer += Time.deltaTime;
            
            if ((_index < 7) && (_againTimer >= _restart[_index] - _timestamps[_index]))
            {
                ++_index;
                audioSource.time = _timestamps[_index];
                _againTimer = 0;
            }
            
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void PlayRewindSound(float time)
    {
        _timer = time;
        _againTimer = 0f;
        _index = 0;
        audioSource.time = 0f;
        audioSource.Play();
    }
}
