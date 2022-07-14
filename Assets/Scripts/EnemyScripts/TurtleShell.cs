using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleShell : MonoBehaviour, IEnemy
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
    bool attacking = false;
    bool dying = false;
    void Start()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("bronze_sword", 5),
            new LootDrop("fire_staff", 5),
            new LootDrop("health_potion", 10)
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
        if (dying == false)
        {
            if (withinAggroColliders.Length > 0)
            {
                ChasePlayer(withinAggroColliders[0].GetComponent<PlayerController>());
            }
        }

        if (GlobalVar.floorNum >= 3)
        {
            DropTable.AddItemToDrop("silver_sword", 2);
            Debug.Log("Adding new weapon to drops");
        }
        if (GlobalVar.floorNum >= 6)
        {
            DropTable.AddItemToDrop("gold_sword", 1);
            Debug.Log("Adding new weapon to drops");
        }
    }

    public void PerformAttack()
    {
        moving = false;
        attacking = true;
        anim.SetBool("Moving", moving);
        Debug.Log("Attacking");
        anim.SetBool("Attack", attacking);
    }

    public void TakeDamage(int amount)
    {
        if (currenthHealth <= 0)
        {
            dying = true;
            attacking = false;
            moving = false;
            anim.SetBool("Moving", moving);
            anim.SetBool("Attack", attacking);
            anim.SetTrigger("Dying");
        }
        else
        {
            Debug.Log("Damage");
            anim.SetTrigger("TakeDamage");
            currenthHealth -= amount;
        }
    }

    void ChasePlayer(PlayerController player)
    {
        Debug.Log("Chasing");
        if (attacking == false)
            moving = true;
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        anim.SetBool("Moving", moving);
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            FaceTarget();
            if (!IsInvoking("PerformAttack"))
            {
                InvokeRepeating("PerformAttack", .5f, 2f);
            }
        }
        else
        {
            CancelInvoke("PerformAttack");
        }
        navAgent.SetDestination(player.transform.position);
    }

    void FaceTarget()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
    public void Die()
    {
        DropLoot();
        CombatEvents.EnemyDied(this);
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
