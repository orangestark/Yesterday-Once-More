using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winObject : MonoBehaviour
{
    public string winScene = "winScene";
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ellen")
        {
            SceneManager.LoadScene(winScene);
        }
    }
}
