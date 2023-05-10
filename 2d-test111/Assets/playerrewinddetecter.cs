using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerrewinddetecter : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject[] gameObjectsToActivate;
    [SerializeField] GameObject[] gameObjectsToDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeBacktutorial myScript = gameObject.GetComponent<TimeBacktutorial>();
        if (myScript.loopcount > 0 && Input.GetKeyDown(KeyCode.Return))
        {
            ActivateGameObjects();
            DeactivateGameObjects();
            
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

