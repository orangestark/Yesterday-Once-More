using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class collidewin : MonoBehaviour
{
    public string sceneName; // Name of the scene to load

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName); // Load the specified scene
        }
    }
}
