using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PushDetecter : MonoBehaviour
{
    public GameObject designatedObject;
    public GameObject[] gameObjectsToActivate;
    public GameObject[] gameObjectsToDeactivate;

    public TMP_Text text;

    public float wait = 3f;

    private static bool _end = false;
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
        if (other.gameObject == designatedObject)
        {
            //ActivateGameObjects();
            //DeactivateGameObjects();
            if (!_end)
                StartCoroutine(EndPhase());
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

    IEnumerator EndPhase()
    {
        _end = true;
        text.text = "Good job! Next let's try something cool...";
        yield return new WaitForSeconds(wait);
        ActivateGameObjects();
        DeactivateGameObjects();
    }
}
