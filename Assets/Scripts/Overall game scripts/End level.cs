using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endlevel : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("Trigger");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
