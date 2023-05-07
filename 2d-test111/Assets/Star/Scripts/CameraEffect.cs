using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public GameObject target;
    public CinemachineVirtualCamera cam;
    public float duration;
    public float FOV;
    private Vector2 _original;
    private float _fovOriginal;
    private Transform _follow;
    private bool _isFirstTime;
    private bool _isWorking;
    private bool _isAvailable = false;
    private bool _goBack;
    private bool _arrived;
    private int _phase = 0;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (_isFirstTime && !DieRoutine.isDead)
        {
            if (!_isWorking)
            {
                _isWorking = true;
                ++_phase;
                if (_phase == 1)
                {
                    StartCoroutine(LerpFromTo(_original, target.transform.position, duration));
                } 
                else if (_phase == 2)
                {
                    StartCoroutine(LerpFromTo(target.transform.position, _original, duration));
                }
                else
                {
                    _isFirstTime = false;
                    _isWorking = false;
                    cam.Follow = _follow;

                }
            }
        } 
        else if (_isAvailable && Input.GetKeyDown(KeyCode.M) && !DieRoutine.isDead)
        {
            if (!_isWorking)
            {
                _isWorking = true;
                _original = cam.transform.position;
                cam.Follow = null; 
                StartCoroutine(LerpFromTo(_original, target.transform.position, duration));
            }
        } 
        else if (_isAvailable && Input.GetKeyUp(KeyCode.M) && !DieRoutine.isDead)
        {
            if (!_isWorking)
            {
                _goBack = true;
                _isWorking = true;
                _arrived = true; 
                StartCoroutine(LerpFromTo(target.transform.position, _original, duration));
            }
        }
    }

    IEnumerator LerpFromTo(Vector2 pos1, Vector2 pos2, float duration)
    {
        for (float t=0f; t<duration; t += Time.deltaTime) 
        {
            if (_goBack)
            {
                _goBack = false;
                yield break;
            }
            cam.transform.position = Vector2.Lerp(pos1, pos2, t / duration);
            Debug.Log("lets goooooooooooooooooooooo");
            yield return 0;
        }
        cam.transform.position = pos2;
        _isWorking = false;
        if (_arrived)
        {
            cam.Follow = _follow;
        }
    }
    
    public void CameraEffectSmooth(GameObject targ, float fov, float d)
    {
        target = targ;
        FOV = fov;
        duration = d;
        _follow = cam.Follow;
        _original = cam.transform.position;
        _fovOriginal = cam.m_Lens.FieldOfView;
        cam.Follow = null;
        _isFirstTime = true;
        _isAvailable = true;
    }
    
}
