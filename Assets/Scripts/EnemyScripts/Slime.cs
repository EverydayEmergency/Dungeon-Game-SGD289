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
    public int Experience { get; set; }
    public DropTable DropTable { get; set; }
    public PickupItem pickupItem;

    //Animation
    private Animator anim;
    bool moving = false;

    

    void Start()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("sword", 25),
            new LootDrop("fire_staff", 25),
            new LootDrop("potion_log", 25)
        };

        player = GameManager.gm.player.GetComponent<PlayerController>();
        Experience = (int)((player.playerLevel.Level * 50) * 1.5);
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(10, 6, 0, 2);
        maxHealth = 30;
        currenthHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        withinAggroColliders = Physics.OverlapSphere(transform.position, 5, aggroLayerMask);
        if (withinAggroColliders.Length > 0)
        {
            ChasePlayer(withinAggroColliders[0].GetComponent<PlayerController>());
        }
    }


    public void PerformAttack()
    {
        Debug.Log("Attacking");
        //anim.SetTrigger("Attacking");
        player.TakeDamage(5);
    }

    public void TakeDamage(int amount)
    {     
        if(currenthHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Damage");
            //anim.SetTrigger("TakeDamage");
            currenthHealth -= amount;
        }
    }  

    void ChasePlayer(PlayerController player)
    {
        Debug.Log("Chasing");
        moving = true;
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        anim.SetBool("Moving", moving);
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
            {
                moving = false;
                //anim.SetBool("Moving", moving);
                InvokeRepeating("PerformAttack", .5f, 2f);
            }
        }
        else
        {      
            CancelInvoke("PerformAttack");
        }
        navAgent.SetDestination(player.transform.position);
    }

    public void Die()
    {
        DropLoot();
        CombatEvents.EnemyDied(this);
        //anim.SetTrigger("dying");
        Destroy(gameObject);
    }

    void DropLoot() 
    {
        Item item = DropTable.GetDrop();
        if (item != null)
        {
            PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
}
