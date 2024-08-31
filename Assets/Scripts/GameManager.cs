using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

        //"Best Score", 0
        //"Money", 0
        //"Multiplier", 1
        //"Energy", 2

        //("Skin", 0);
        //("Square", true);
        //("Hexagon", false);
        //("Plus", false);
        //("Star", false);
        //("Shuriken", false);
}
