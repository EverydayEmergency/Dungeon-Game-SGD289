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
    public AudioSource squish;
    public int Experience { get; set; }
    public DropTable DropTable { get; set; }
    public PickupItem pickupItem;

    //Animation
    private Animator anim;
    bool moving = false;
    bool attacking = false;
    bool dying = false;
    bool hit = false;
    void Start()
    {
        DropTable = new DropTable();
        DropTable.loot = new List<LootDrop>
        {
            new LootDrop("bronze_sword", 5),
            new LootDrop("fire_staff", 5),
            new LootDrop("health_potion", 15)
        };

        player = GameManager.gm.player.GetComponent<PlayerController>();
        Experience = (int)((player.playerLevel.Level * 50) * 1.5);
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(10, 6, 0, 2);
        if (GlobalVar.floorNum <= 5)
            maxHealth = 25 * GlobalVar.floorNum;
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
        }
        if (GlobalVar.floorNum >= 6)
        {
            DropTable.AddItemToDrop("gold_sword", 1);
        }
    }

    public void PerformAttack()
    {
        
        moving = false;
        anim.SetBool("Moving", moving);
        anim.SetTrigger("Attacking");
    }

    public void playerHit()
    {
        if (hit)
        {
            player.currentHealth -= 5;
            hit = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (attacking == true && moving == false)
        {
            if (other.gameObject.tag == "Player")
            {
                hit = true;
            }
        }
    }

    IEnumerator wait()
    {
        
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(3);
        attacking = true;
      
    }

    public void TakeDamage(int amount)
    {
        if (currenthHealth <= 0)
        {
            dying = true;
            attacking = false;
            moving = false;
            anim.SetBool("Moving", moving);
            squish.Play(0);
            anim.SetTrigger("Dying");
        }
        else
        {
            anim.SetTrigger("TakeDamage");
            squish.Play(0);
            currenthHealth -= amount;
        }
    }

    void ChasePlayer(PlayerController player)
    {
        if (attacking == false)
            moving = true;
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        anim.SetBool("Moving", moving);
        FaceTarget();
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
            {
                attacking = true;
                InvokeRepeating("PerformAttack", 0.5f, 2f);
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
            Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            PickupItem instance = Instantiate(pickupItem, spawnPos, Quaternion.identity);
            instance.ItemDrop = item;
        }
    }
}