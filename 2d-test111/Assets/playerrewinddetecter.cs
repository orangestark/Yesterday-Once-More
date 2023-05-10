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
    

    // Start is called before the first frame update
    void Start()
    {
        
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
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ActivateGameObjects();
                DeactivateGameObjects();
                myScript.loopcount = 0;
            }

            //textComponent.text = "Now your action will be looping!\n \nPress <font=\"Rajdhani-Bold SDF\">SHIFT</font> again and try more!\n \nor Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
            textComponent.text = "Now your action will be looping!\n \nTRY MORE or Press <font=\"Rajdhani-Bold SDF\">ENTER</font> to the next tutorial";
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

