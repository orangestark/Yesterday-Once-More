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
    private bool phase5 = false;


    //private float boxpushcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeBackObjecttutorial boxScript = box.GetComponent<TimeBackObjecttutorial>();
        TimeBacktutorial myScript = player.GetComponent<TimeBacktutorial>();
        if ( boxScript.pushcount > 0)
        {
            Debug.Log(rewindcount);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                boxcount = boxScript.pushcount;
                rewindcount = myScript.loopcount;
                ActivateGameObjects();
                textComponent.text = "Now push the box to the Kiosks one by one\n \nIf your box is still looping\n \nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> TWICE to refresh the loop";
                phase5 = true;
            }

            if (firsttime)
            {
                textComponent.text = "Now the position of the box will also be looping!\n \n Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
                firsttime = false;
            }

            if (phase5 && myScript.loopcount - rewindcount > 0 && boxScript.pushcount - boxcount == 0)
            {
                textComponent.text = "Now push the box to the Kiosks one by one";
                phase5 = false;
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

