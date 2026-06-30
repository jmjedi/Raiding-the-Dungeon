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
            string dir = null;
            Vector3 normal = collision.GetContact(0).normal;

            if (Mathf.Abs(normal.y) > 0.5f)
            {
                if (normal.y > 0) dir = "Top";
                else dir = "Bottom";
            }
            else if (Mathf.Abs(normal.x) > 0.5f)
            {
                if (normal.x > 0) dir = "Right";
                else dir = "Left";
            }
            else if (Mathf.Abs(normal.z) > 0.5f)
            {
                if (normal.z > 0) dir = "Front";
                else dir = "Back";
            }

            PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>(); //Get player hp
            if (playerHP != null)
                playerHP.Damage(damage, dir); //Damage player
            else
                Debug.Log("NO PLAYER HP EXISTS");
        }
    }
}
