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

    private void OnTriggerEnter(Collider other)
    {
        print("Work now plz");
        if (other.gameObject.CompareTag("Enemy"))
        {
            print("KILL IT WITH FIRE");
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            plrUI.ChangeGold(5);
            Destroy(other.gameObject);
        }
    }

}
