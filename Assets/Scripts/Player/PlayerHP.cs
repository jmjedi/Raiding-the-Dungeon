using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    public float maxHP = 100;
    public float HP;
    public float hit_debounce;
    private PlayerUI plrUI;

    private void Start()
    {
        //Set player HP
        HP = maxHP;
        plrUI = Object.FindAnyObjectByType<PlayerUI>();
    }

    public float GetHP()
    {
        //Find player HP
        return HP;
    }

    private void Update()
    {
        //Update Debounce Values
        PlayerController playerControl = GetComponent<PlayerController>();
        if (hit_debounce > 0)
        {
            hit_debounce -= 1 * Time.deltaTime;
        }
     }

    public void Damage(float Damage)
    {
        //Lose player HP
        if (hit_debounce > 0) return;

        HP -= Damage;
        hit_debounce = 2f;
        PlayerController playerControl = GetComponent<PlayerController>();
        plrUI.ChangeHP(HP);

        if (playerControl != null)
            playerControl.Damaged();
        else
            print("NO PLAYER CONTROL");

        if (HP <= 0) //We are dead
        {
            HP = 0;
            SceneManager.LoadScene(4);
        }
    }
}
