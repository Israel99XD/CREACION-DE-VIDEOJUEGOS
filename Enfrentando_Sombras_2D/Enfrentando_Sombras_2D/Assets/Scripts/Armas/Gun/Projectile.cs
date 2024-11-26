using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public float speed = 1;

    [HideInInspector]
    public Weapon weapon;

    void Awake()
    {
        var body = this.GetComponent<Rigidbody2D>();
        body.velocity = this.transform.up * this.speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);

        var health = other.gameObject.GetComponent<Healt>();

        if (health != null)
            this.weapon.OnHit(health, this.gameObject);
    }
}
