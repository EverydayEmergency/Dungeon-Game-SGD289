using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public int CurrentDamage { get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PerformAttack(int damage)
    {
        CurrentDamage = damage;
        animator.SetTrigger("Base_Attack");
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "Enemy")
        {
            collider.GetComponent<IEnemy>().TakeDamage(CurrentDamage);
        }
    }
}
