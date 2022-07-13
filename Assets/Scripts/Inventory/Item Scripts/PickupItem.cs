using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{
    public Item ItemDrop { get; set; }
    public Animator anim;

    private void Start()
    {
        if(TryGetComponent(out Animator animator))
        {
            anim = animator;
        }
    }
    public override void Interact()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        if(anim != null)
        {
            anim.SetTrigger("PickedUp");
        }
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }
}
