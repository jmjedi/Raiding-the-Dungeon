using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    private PlayerUI plrUI;

    // Start is called before the first frame update
    void Start()
    {
        plrUI = Object.FindAnyObjectByType<PlayerUI>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) //Check if hitbox is touching player
        {
            plrUI.ChangeGold(5);
            Destroy(collision.gameObject);
        }
    }

}
