using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class EnemyManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform Player;
    public GameObject attackObj;

    private float attack_debounce;

    private void Update()
    {
        if (attack_debounce > 0)
            attack_debounce -= 1f * Time.deltaTime;

        agent.SetDestination(Player.position);
        transform.LookAt(Player);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    AttackActive();
                }
            }
        }
    }

    private void AttackActive()
    {
        if (attack_debounce > 0) return;
        Vector3 worldOffset = transform.rotation * new Vector3(0, 0, 1.5f);
        GameObject spawnedObject = Instantiate(attackObj, transform.position + worldOffset, transform.rotation);

        attack_debounce = 1.7f;
        Destroy(spawnedObject, 0.1f);
    }
}
