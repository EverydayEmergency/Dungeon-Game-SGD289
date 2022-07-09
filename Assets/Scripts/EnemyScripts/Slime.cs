using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slime : MonoBehaviour, IEnemy
{
    public LayerMask aggroLayerMask;
    public int currenthHealth;
    public int maxHealth;

    private PlayerController player;
    private NavMeshAgent navAgent;
    private CharacterStats characterStats;
    private Collider[] withinAggroColliders;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(10, 6, 0, 2);
        currenthHealth = maxHealth;
    }

    void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, 10, aggroLayerMask);
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<PlayerController>());
        }
    }


    public void PerformAttack()
    {
        player.TakeDamage(5);
    }

    public void TakeDamage(int amount)
    {
        currenthHealth -= amount;
        if(currenthHealth <= 0)
        {
            Die();
        }
    }  

    void ChasePlayer(PlayerController player)
    {        
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if(!IsInvoking("PerformAttack"))
                InvokeRepeating("PerformAttack", .5f, 2f);
        }
        else
        {      
            CancelInvoke("PerformAttack");
        }
        navAgent.SetDestination(player.transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
