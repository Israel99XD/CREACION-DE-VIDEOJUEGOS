using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGunWeapon : GunWeapon
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [Header("Projectile Gun Weapon")]
    public GameObject projectilePrefab;

    protected override void ShootProjectile(Vector3 position, Vector3 direction)
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }


        float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90;
        var rotation = Quaternion.Euler(0, 0, angle);

        var bullet = Instantiate(this.projectilePrefab, position, rotation);

        bullet.GetComponent<Projectile>().weapon = this;
    } 
}
