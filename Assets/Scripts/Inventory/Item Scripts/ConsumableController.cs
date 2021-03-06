using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerController>().characterStats;
    }

    public void ConsumeItem(Item item)
    {
       
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
        if (item.ItemModifier)
        {       
            itemToSpawn.GetComponent<IConsumable>().Comsume(stats);
        }

        else
            itemToSpawn.GetComponent<IConsumable>().Consume();
    }

}
