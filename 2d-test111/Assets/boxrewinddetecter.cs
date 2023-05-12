using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class boxrewinddetecter : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] gameObjectsToActivate;
    [SerializeField] GameObject[] gameObjectsToDeactivate;
    public TMP_Text textComponent;
    private bool firsttime = true;
    private float rewindcount =0;
    private float boxcount;
    private bool next_phase = false;
    private float temp;
    [SerializeField] private Transform startingPoint;
    private int canvasState = 0;


    //private float boxpushcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (Controller.usingController)
        {
            textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">LB/RB</font> and then push the box\n\nPress <font=\"Rajdhani-Bold SDF\">LB/RB</font> again and witness another miracle!\n\nTry using the portal if the box is stuck at the wall. ";
        }
        else
        {
            textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">SHIFT</font> and then push the box\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and witness another miracle!\n\nTry using the portal if the box is stuck at the wall. ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasState == 0)
        {
            if (Controller.usingController)
            {
                textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">LB/RB</font> and then push the box\n\nPress <font=\"Rajdhani-Bold SDF\">LB/RB</font> again and witness another miracle!\n\nTry using the portal if the box is stuck at the wall. ";
            }
            else
            {
                textComponent.text = "Press <font=\"Rajdhani-Bold SDF\">SHIFT</font> and then push the box\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and witness another miracle!\n\nTry using the portal if the box is stuck at the wall. ";
            }
        }
        else if (canvasState == 1)
        {
            if (Controller.usingController)
            {
                textComponent.text = "Now the position of the box is also looping!\n\nHowever, you cannot push a box in loop\n\nPress <font=\"Rajdhani-Bold SDF\">LB/RB</font> TWICE to refresh the loop";
            }
            else
            {
                textComponent.text = "Now the position of the box is also looping!\n\nHowever, you cannot push a box in loop\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> TWICE to refresh the loop";
            }
        } 
        else if (canvasState == 2)
        {
            if (Controller.usingController)
            {
                textComponent.text = "The box can be pushed again now! Good job!!!\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">X</font> to end the tutorial\n\n\n";
            }
            else
            {
                textComponent.text = "The box can be pushed again now! Good job!!!\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to end the tutorial\n\n\n";
            }
        }
        TimeBackObjecttutorial boxScript = box.GetComponent<TimeBackObjecttutorial>();
        TimeBacktutorial myScript = player.GetComponent<TimeBacktutorial>();
        if ( boxScript.pushcount > 0)
        {
            boxcount = boxScript.pushcount;
            rewindcount = myScript.loopcount;
            //Debug.Log(rewindcount + " " + boxcount);
            /*
            if (Input.GetKeyDown(KeyCode.Return))
            {
                boxcount = boxScript.pushcount;
                rewindcount = myScript.loopcount;
                ActivateGameObjects();
                textComponent.text = "Now push the box to the Kiosks one by one\n \nIf your box is still looping\n \nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> TWICE to refresh the loop";
                phase5 = true;
            }*/

            if (firsttime)
            {
                //textComponent.text = "Now the position of the box is also looping!\n\nHowever, you cannot push a box in loop\n\nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> TWICE to refresh the loop";
                canvasState = 1;
                firsttime = false;
                temp = rewindcount;
            }
            else if (!next_phase && (rewindcount - 1 == temp))
            {
                //textComponent.text = "The box can be pushed again now! Good job!!!\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to end the tutorial\n\n\n";
                canvasState = 2;
                next_phase = true;
            }
            else if (next_phase && ((Input.GetKeyDown(KeyCode.Return) && !Controller.usingController) ||
                                    (Input.GetButtonDown("X") && Controller.usingController)))
            {
                player.transform.position = startingPoint.position;
                ActivateGameObjects();
                DeactivateGameObjects();
            }

            /*
            if (phase5 && myScript.loopcount - rewindcount > 0 && boxScript.pushcount - boxcount == 0)
            {
                textComponent.text = "Now push the box to the Kiosks one by one";
                phase5 = false;
            }*/
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

