using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRoutine : MonoBehaviour
{
    public static bool isDead = false;
    public static bool isLocked = false;
    
    [SerializeField] private TimeBack timeBack;
    [SerializeField] private TimeBackObject[] timeBackObjects;
    [SerializeField] private TimeBackLiftable[] timeBackLiftables;
    [SerializeField] private ShowStatus showStatus;

    public void StartDieRoutine()
    {
        isDead = true;
        timeBack.Reset();
        foreach (var timeBackObject in timeBackObjects)
        {
            timeBackObject.Reset();
        }
        foreach (var timeBackLiftable in timeBackLiftables)
        {
            timeBackLiftable.Reset();
        }
        showStatus.Pause();
    }
    
    public void StartRespawnRoutine()
    {
        timeBack.Restart();
        foreach (var timeBackObject in timeBackObjects)
        {
            timeBackObject.Restart();
        }
        foreach (var timeBackLiftable in timeBackLiftables)
        {
            timeBackLiftable.Restart();
        }
        showStatus.Restart();
        isDead = false;
    }
    
    public void StartLockRoutine()
    {
        isLocked = true;
        /*
        timeBack.Reset();
        foreach (var timeBackObject in timeBackObjects)
        {
            timeBackObject.Reset();
        }
        foreach (var timeBackLiftable in timeBackLiftables)
        {
            timeBackLiftable.Reset();
        }*/
        TimeBack.PauseRewind();
        showStatus.Pause();
    }
    
    public void StartUnlockRoutine()
    {
        /*
        timeBack.Restart();
        foreach (var timeBackObject in timeBackObjects)
        {
            timeBackObject.Restart();
        }
        foreach (var timeBackLiftable in timeBackLiftables)
        {
            timeBackLiftable.Restart();
        }*/
        TimeBack.PlayRewind();
        showStatus.Restart();
        isLocked = false;
    }
}
