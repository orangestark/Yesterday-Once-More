using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
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
