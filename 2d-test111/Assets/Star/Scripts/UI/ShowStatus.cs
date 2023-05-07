using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowStatus : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private TimeBack timeback;
    private bool isRecording;
    private bool isRewinding;
    private bool isForwarding;
    private float timeRemaining;
    private int _dots;
    private bool _start;
    private float _localtime;
    [SerializeField] float max_time = 0.3f;

    private string _original;
    // Start is called before the first frame update
    void Start()
    {
        _start = false;
        _localtime = max_time;
        _original = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        isRecording = timeback.isRecording;
        isRewinding = timeback.isRewinding;
        isForwarding = timeback.isForwarding;
        timeRemaining = timeback.timeRemaining;
        if (isRecording)
        {
            text.text = string.Format("<color=\"black\">Shadow</color> Recording\nRemaining time:<color=\"blue\"><size=50>{0,2:0}</size></color>s", timeRemaining);
        } 
        else if (isRewinding)
        {
            if (!_start)
            {
                text.text = "Rewinding...";
                _dots = 3;
                _start = true;
            }
            else
            {
                _localtime -= Time.deltaTime;
                if (_localtime <= 0)
                {
                    _localtime = max_time;
                    _dots = (_dots + 3) % 4;
                    if (_dots == 3)
                    {
                        text.text = "Rewinding...";
                    }
                    else if (_dots == 2)
                    {
                        text.text = "Rewinding..<color=#00000000>.</color>";
                    }
                    else if (_dots == 1)
                    {
                        text.text = "Rewinding.<color=#00000000>..</color>\n";
                    }
                    else if (_dots == 0)
                    {
                        text.text = "Rewinding<color=#00000000>...</color>\n";
                    }
                }
            }
            
            
        } 
        else if (isForwarding)
        {
            text.text = string.Format("<color=\"black\">Shadow</color> <color=#55F1FF>Activated</color>");
        }
    }

    public void Restart()
    {
        text.text = _original;
    }
}
