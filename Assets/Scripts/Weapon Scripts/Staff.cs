using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    private Animator animator;
    public List<BaseStat> Stats { get; set; }
    public int CurrentDamage { get; set; }
    public Transform ProjectileSpawn { get; set; }
    Fireball fireball;
    public Transform spawnPoint;

    void Start()
    {
        fireball = Resources.Load<Fireball>("Weapons/Projectiles/Fireball");
        ProjectileSpawn = spawnPoint;//new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + 0.35f);
        animator = GetComponent<Animator>();
    }
    public void PerformAttack(int damage)
    {
        animator.SetTrigger("Base_Attack");
    }

    public void CastProjectile()
    {
        Fireball fireballInstance = Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.forward;
    }
}
