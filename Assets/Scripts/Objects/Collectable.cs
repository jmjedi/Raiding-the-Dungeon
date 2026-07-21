using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    private PlayerUI plrUI;
    
    // Start is called before the first frame update
    void Start()
    {
        plrUI = Object.FindAnyObjectByType<PlayerUI>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Check if hitbox is touching player
        {
            plrUI.ChangeGold(1);
            Destroy(gameObject);
        }
    }
}
