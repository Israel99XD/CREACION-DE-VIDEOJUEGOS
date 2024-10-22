using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

    private float time = 0;
    public float aiTimeOut = .5f;

    [Header("Combat")]
    public float attackRange = 1f;

    [Header("View")]
    public Transform viewPoint;
    public float viewRadius = 1f;

    public LayerMask whoIsMyTarget;
    public GameObject gfx;

    private Rigidbody2D body;
    private Vector3 movement;
    private bool lookingRight = true;

    private bool isGun = false;

    private Weapon weapon;
    private Animator Animator;

    void Start()
    {
        this.body = this.GetComponent<Rigidbody2D>();
        this.weapon = this.GetComponentInChildren<Weapon>();
        Animator = this.gfx.GetComponent<Animator>();
        if (this.weapon is GunWeapon)
        {
            (this.weapon as GunWeapon).currentAmmo = 9999;
            this.isGun = true;
        }
    }

    void Update()
    {
        this.time += Time.deltaTime;

        if (this.time > this.aiTimeOut)
        {
            this.time = 0;

            this.AIRutine();
        }

        this.movement.y = this.body.velocity.y;
        this.body.velocity = this.movement;
    }

    void AIRutine()
    {
        this.movement = Vector3.zero;

        // Dettect target
        Collider2D posibleTarget = Physics2D.OverlapCircle(
            this.viewPoint.position,
            this.viewRadius,
            this.whoIsMyTarget
        );

        if (posibleTarget != null)
        {
            Vector3 targetPosition = posibleTarget.gameObject.transform.position;

            // Move to
            Vector3 direction = (targetPosition - this.transform.position).normalized;
            float horizontalDirection = Mathf.Sign(direction.x);

            this.movement.x = horizontalDirection * this.speed;
            this.FlipSprite(this.movement.x);

            if (!isGun)
            {
                Animator.SetBool("running", horizontalDirection != 0.0f);
            }
            

            // Attack
            float rangeSqr = this.attackRange * this.attackRange;
            float distanceToTarget = (this.transform.position - targetPosition).sqrMagnitude;
            bool isInRange = distanceToTarget < rangeSqr;
            
            

            if (isInRange)
            {
                if (!isGun) 
                {
                    Animator.SetBool("running", false);
                    
                }
                Animator.SetBool("atack", true);
                this.weapon.Active();
                this.movement.x = 0;
            }
            else{
                if (!isGun)
                {
                    Animator.SetBool("running", true);
                }
                Animator.SetBool("atack", false);
            }
            
        }
    }

    void FlipSprite(float direction)
    {
        if (direction == 0)
            return;

        // Got to right
        if (direction > 0 && this.lookingRight == false)
        {
            this.lookingRight = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        // Got to left
        else if (direction < 0 && this.lookingRight == true)
        {
            this.lookingRight = false;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnDrawGizmos()
    {
        if (this.viewPoint != null)
        {
            Gizmos.DrawWireSphere(this.viewPoint.position, this.viewRadius);
        }

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, this.attackRange);
    }
}
