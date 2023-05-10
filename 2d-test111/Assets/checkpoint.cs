using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class checkpoint : MonoBehaviour
{
    public GameObject kiosk1;
    public GameObject kiosk2;

    public float checkcount = 0;

    private bool firsttime = true;
    private bool firsttime2 = true;
    public GameObject[] gameObjectsToActivate;
    public GameObject[] gameObjectsToDeactivate;
    public TMP_Text textComponent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checkcount == 2)
        {
            ActivateGameObjects();
            DeactivateGameObjects();
        } else if (checkcount ==1)
        {
            textComponent.text = "You have already pushed the box to one kiosk, try pushing it to the other\n \nTry going through the portal\n \nIf your box is still looping, Press SHIFT twice";
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == kiosk1 && firsttime)
        {
            checkcount += 1;
            firsttime = false;
            Debug.Log(checkcount);
        } else if (other.gameObject == kiosk2 && firsttime2)
        {
            checkcount += 1;
            firsttime2 = false;
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
