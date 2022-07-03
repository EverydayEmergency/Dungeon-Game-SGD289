using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour{
    public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    CharacterStats characterStats;

    IWeapon equippedWeapon;
    void Start()
    {
        characterStats = GetComponent<CharacterStats>();

    }
    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            characterStats.RemoveStatBonus(EquippedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), 
            playerHand.transform.position, playerHand.transform.rotation);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        equippedWeapon.Stats = itemToEquip.Stats; //To know what stats to remove upon being equipped
        EquippedWeapon.transform.SetParent(playerHand.transform); //Parent is now the player hand
        characterStats.AddStatBonus(itemToEquip.Stats);

        //Debug.Log(equippedWeapon.Stats[0].GetCalculatedStatValue());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformWeaponAttack();
        }   
    }

    public void PerformWeaponAttack()
    {
        equippedWeapon.PerformAttack();
    }
}
