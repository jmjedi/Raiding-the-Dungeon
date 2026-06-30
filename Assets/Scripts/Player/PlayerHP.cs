using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100;
    private float HP;

    private void Start()
    {
        //Set player HP
        HP = maxHP;
    }

    public float GetHP()
    {
        //Find player HP
        return HP;
    }

    public void Damage(float Damage)
    {
        //Lose player HP
        HP -= Damage;
        PlayerController playerControl = GetComponent<PlayerController>();
        if (playerControl != null)
            playerControl.Damaged();
        else
            print("NO PLAYER CONTROL");

        if (HP <= 0) //We are dead
        {
            HP = 0;
            //GET SCENE MANAGER HERE
        }
    }
}
