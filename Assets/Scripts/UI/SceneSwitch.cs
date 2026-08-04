using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public void SwitchSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SwitchSceneByIndex(int sceneIndx)
    {
        SceneManager.LoadScene(sceneIndx);
    }
}
