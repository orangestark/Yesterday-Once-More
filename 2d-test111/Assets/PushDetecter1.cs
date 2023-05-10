using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PushDetecter1 : MonoBehaviour
{
    public GameObject player;
    public GameObject designatedObject;
    public GameObject[] gameObjectsToActivate;
    public GameObject[] gameObjectsToDeactivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        TimeBacktutorial myScript = gameObject.GetComponent<TimeBacktutorial>();
        if (other.gameObject == designatedObject && myScript.rewindcount == 1)
        {
            ActivateGameObjects();
           DeactivateGameObjects();
           myScript.rewindcount = 0;
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
