using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    public PlayerWeaponController playerWeaponController;
    public ConsumableController consumableController;
    public Item sword;
    public Item staff;
    public Item PotionLog;

    void Start()
    {
        playerWeaponController = GetComponent<PlayerWeaponController>();
        consumableController = GetComponent<ConsumableController>();
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power", "Your power level"));
        sword = new Item(swordStats, "sword");
        staff = new Item(swordStats, "fire_staff");

        PotionLog = new Item(new List<BaseStat>(), "potion_log", "Drink this to log something cool!", "Drink", "Log Potion", true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerWeaponController.EquipWeapon(sword);
            consumableController.ConsumeItem(PotionLog);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerWeaponController.EquipWeapon(staff);           
        }        
    }
}
