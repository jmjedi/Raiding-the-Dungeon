using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Start()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
    void ExittGame()
    {

    }
}
