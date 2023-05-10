using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxrewinddetecter : MonoBehaviour
{
    [SerializeField] GameObject box;
    [SerializeField] GameObject[] gameObjectsToActivate;
    [SerializeField] GameObject[] gameObjectsToDeactivate;

    //private float boxpushcount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeBackObjecttutorial boxScript = box.GetComponent<TimeBackObjecttutorial>();
        if (Input.GetKeyDown(KeyCode.Return) && boxScript.pushcount > 0)
        {
            ActivateGameObjects();
            //DeactivateGameObjects();
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

