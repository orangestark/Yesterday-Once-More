using System;
using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winObject : MonoBehaviour
{
    public string winScene = "winScene";
    //[SerializeField] private Checkpoint respawnPoint;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Ellen")
        {
            PlayerCharacter c = col.GetComponent<PlayerCharacter>();
            //c.SetChekpoint(respawnPoint);
            SceneManager.LoadScene(winScene);
        }
    }
}
