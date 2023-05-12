using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class playerrewinddetecter : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject[] gameObjectsToActivate;
    [SerializeField] GameObject[] gameObjectsToDeactivate;
    public TMP_Text textComponent;
    [SerializeField] private GameObject canvasRewind;
    [SerializeField] private Transform startingPoint;


    // Start is called before the first frame update
    void Start()
    {
        if (Controller.usingController)
        {
            textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">LB/RB</font> to start recording your action\n\nPress <font=\"Rajdhani-Bold SDF\">LB/RB</font> again and witness the miracle!";
        }
        else
        {
            textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">SHIFT</font> to start recording your action\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and witness the miracle!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        canvasRewind.SetActive(true);
        //textComponent = GetComponent<TMP_Text>();
        TimeBacktutorial myScript = gameObject.GetComponent<TimeBacktutorial>();
        myScript.enabled = true;
        if (myScript.loopcount > 0)
        {
            
            if ((Input.GetKeyDown(KeyCode.Return) && !Controller.usingController) || (Input.GetButtonDown("X") && Controller.usingController))
            {
                gameObject.transform.position = startingPoint.position;
                ActivateGameObjects();
                DeactivateGameObjects();
                myScript.loopcount = 0;
            }

            //textComponent.text = "Now your action will be looping!\n \nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and try more!\n \nor Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
            //textComponent.text = "Now your action is looping! You can record up to 10 seconds\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
            if (Controller.usingController)
            {
                textComponent.text = "Now your action is looping! You can record up to 10 seconds\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">X</font> to the next tutorial";
            }
            else
            {
                textComponent.text = "Now your action is looping! You can record up to 10 seconds\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
            }
        }
        else
        {
            if (Controller.usingController)
            {
                textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">LB/RB</font> to start recording your action\n\nPress <font=\"Rajdhani-Bold SDF\">LB/RB</font> again and witness the miracle!";
            }
            else
            {
                textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">SHIFT</font> to start recording your action\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and witness the miracle!";
            }
        }
    }
    void ActivateGameObjects()
    {
        foreach (GameObject go in gameObjectsToActivate)
        {
            go.SetActive(true);
        }
    }

    void DeactivateGameObjects()
    {
        foreach (GameObject go in gameObjectsToDeactivate)
        {
            go.SetActive(false);
        }
    }
}

