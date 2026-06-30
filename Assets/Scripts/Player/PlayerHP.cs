using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100;
    float HP;

    private void Start()
    {
        HP = maxHP;
    }

    public float GetHP()
    {
        return HP;
    }

    public void Damage(float Damage)
    {
        HP -= Damage;
        if (HP < 0)
        {
            HP = 0;
            //GET SCENE MANAGER HERE
        }
    }
}
