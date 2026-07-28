using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheLevelEnd : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Check if hitbox is touching player
        {
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
    }
}
