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
            print("SWITCH");
            PlayerPrefs.SetFloat("Gold", collision.gameObject.GetComponent<PlayerController>().Gold);
            PlayerPrefs.SetFloat("HP", collision.gameObject.GetComponent<PlayerHP>().HP);
            SceneManager.LoadScene(2);
            Destroy(gameObject);
        }
    }
}
