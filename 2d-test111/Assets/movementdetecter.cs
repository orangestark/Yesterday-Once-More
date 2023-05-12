using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class movementdetecter : MonoBehaviour
{
    private bool lefted = false;

    private bool righted = false;

    private bool jumped = false;
    public GameObject[] gameObjectsToActivate;
    public GameObject[] gameObjectsToDeactivate;
    [SerializeField] GameObject player;

    [SerializeField] private TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {   
        if (Controller.usingController)
        {
            text.text = "Welcome to the tutorial!\n\nPush <font=\"Rajdhani-Bold SDF\">LS</font> to MOVE, and Press<font=\"Rajdhani-Bold SDF\">A</font> to JUMP";
        }
        else
        {
            text.text = "Welcome to the tutorial!\n\nPress <font=\"Rajdhani-Bold SDF\">A</font> to go LEFT, <font=\"Rajdhani-Bold SDF\">D</font> to go RIGHT, <font=\"Rajdhani-Bold SDF\">SPACE</font> to JUMP";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.usingController)
        {
            text.text = "Welcome to the tutorial!\n\nPush <font=\"Rajdhani-Bold SDF\">LS</font> to MOVE, and Press <font=\"Rajdhani-Bold SDF\">A</font> to JUMP";
            if (Input.GetButtonUp("A"))
            {
                jumped = true;
                Debug.Log("jump");
            }
            else if (Input.GetAxisRaw("Leftstick Horizontal") > 0.2)
            {
                lefted = true;
                Debug.Log("left");
            } else if (Input.GetAxisRaw("Leftstick Horizontal") < -0.2)
            {
                righted = true;
                Debug.Log("right");
            }
        }
        else
        {
            text.text = "Welcome to the tutorial!\n\nPress <font=\"Rajdhani-Bold SDF\">A</font> to go LEFT, <font=\"Rajdhani-Bold SDF\">D</font> to go RIGHT, <font=\"Rajdhani-Bold SDF\">SPACE</font> to JUMP";
            if (Input.GetKeyUp(KeyCode.A))
            {
                lefted = true;
            } else if (Input.GetKeyUp(KeyCode.D))
            {
                righted = true;
            } else if (Input.GetKeyUp(KeyCode.Space))
            {
                jumped = true;
            }
        }
        
        TimeBacktutorial myScript = player.GetComponent<TimeBacktutorial>();
        myScript.enabled = false;
        if (lefted && righted &&  jumped)
        {
            ActivateGameObjects();
            DeactivateGameObjects();
        }
    }
    
    public void ActivateGameObjects()
    {
        foreach (GameObject go in gameObjectsToActivate)
        {
            go.SetActive(true);
        }
    }

    public void DeactivateGameObjects()
    {
        foreach (GameObject go in gameObjectsToDeactivate)
        {
            go.SetActive(false);
        }
    }
}
