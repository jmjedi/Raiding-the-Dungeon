using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    PlayerHP playerHP;
    [SerializeField] private float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        playerHP = GameObject.Find("Character").GetComponent<PlayerHP>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        var plrHP = collision.gameObject.GetComponent<PlayerHP>();
        if (plrHP)
        {
            DamagePlayer();
        }
    }

    void DamagePlayer()
    {
        playerHP.Damage(damage);
    }
}
