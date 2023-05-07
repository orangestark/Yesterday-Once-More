using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRoutine : MonoBehaviour
{
    [SerializeField] private TimeBack timeBack;
    [SerializeField] private TimeBackObject[] timeBackObjects;
    [SerializeField] private TimeBackLiftable[] timeBackLiftables;
    [SerializeField] private ShowStatus showStatus;

    public void StartDieRoutine()
    {
        timeBack.Reset();
        foreach (var timeBackObject in timeBackObjects)
        {
            timeBackObject.Reset();
        }
        foreach (var timeBackLiftable in timeBackLiftables)
        {
            timeBackLiftable.Reset();
        }
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
    }
}
