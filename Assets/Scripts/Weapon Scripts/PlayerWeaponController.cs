using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }
    Item previouslyEquippedItem;
    CharacterStats characterStats;
    Transform spawnProjectile;
    Transform startSpawnPoint;
    IWeapon equippedWeapon;

    void Start()
    {
        spawnProjectile = transform.Find("ProjectileSpawn");
        startSpawnPoint = spawnProjectile;
        characterStats = GetComponent<PlayerController>().characterStats;
    }
    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
                spawnProjectile = startSpawnPoint;
            UnequipWeapon();
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug),
            playerHand.transform.position, playerHand.transform.rotation);

        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();

        //if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        //    EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile; //If a weapon has a projectile spawn

        EquippedWeapon.transform.SetParent(playerHand.transform); //Parent is now the player hand
        equippedWeapon.Stats = itemToEquip.Stats; //To know what stats to remove upon being equipped   
        previouslyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);        
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(previouslyEquippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
        Destroy(EquippedWeapon.transform.gameObject);
        UIEventHandler.StatsChanged();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            PerformWeaponAttack();
        }
    }

    public void PerformWeaponAttack()
    {
        equippedWeapon.PerformAttack(CalculateDamage());
    }

    private int CalculateDamage()
    {
        int attack = characterStats.GetStat(BaseStat.BaseStatType.Attack).GetCalculatedStatValue();
        
        int damageToDeal = (attack * 2) + Random.Range(2, 10);
        damageToDeal += CalculateCrit(damageToDeal);
        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if(Random.value <= .10f)
        {
            int critDamage = (int)(damage * Random.Range(.5f, .75f));
            return critDamage;
        }
        return 0;
    }
}
