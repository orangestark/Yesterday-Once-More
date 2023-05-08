using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class CameraEffectTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _never = true;
    [SerializeField] private CameraEffect cameraEffect;
    [SerializeField] private GameObject target;
    [SerializeField] private float FOV = 60f;
    [SerializeField] private float duration = 2f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_never)
        {
            PlayerCharacter c = col.GetComponent<PlayerCharacter>();
            if (c != null)
            {
                cameraEffect.CameraEffectSmooth(target, FOV, duration);
                _never = false;
            }
        }
    }
}
