using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winGame : MonoBehaviour
{
    public string gameScene = "finalScene";

    public string winScene = "winScene";
    // Start is called before the first frame update
    public void PlayAgain()
    {
        SceneManager.LoadScene(gameScene);
    }
    
    public void YouWin()
    {
        SceneManager.LoadScene(winScene);
    }
}
