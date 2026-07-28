using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLevelEnd : MonoBehaviour
{
    public string sceneName;

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
