using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    [SerializeField] private float damage = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) //Check if hitbox is touching player
        {
            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>(); //Get player hp
            if (playerHP != null)
                playerHP.Damage(damage); //Damage player
            else
                Debug.Log("NO PLAYER HP EXISTS");
        }
    }
}
