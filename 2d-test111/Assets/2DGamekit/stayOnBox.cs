using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayOnBox : MonoBehaviour
{

    public GameObject player;
    public GameObject box;
       

    void OnTriggerStay2D(Collider2D col)
    {
        
        player.transform.parent = box.transform;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        player.transform.parent = null;
    }
}
