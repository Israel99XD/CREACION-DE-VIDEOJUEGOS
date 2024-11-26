using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CQCWeapon : Weapon
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public Transform hitSpot;
    public float range = 1;

    public Transform gfx;
    protected override void OnActive()
    {
        StartCoroutine(this.Animate());


        Collider2D[] targets = Physics2D.OverlapCircleAll(this.hitSpot.position, this.range);

        foreach (var target in targets)
        {
            Healt h = target.GetComponent<Healt>();
            if (h == null)
                continue;

            this.OnHit(h, this.gameObject);
        }
    }
    IEnumerator Animate()
    {
        if (audioSource != null) {
            audioSource.Play();
        }
        


        this.gfx.localRotation = Quaternion.Euler(0, 0, -35);

        yield return new WaitForSeconds(this.maxCooldownTime * 0.9f);

        this.gfx.localRotation = Quaternion.Euler(0, 0, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.hitSpot.position, this.range);
    }
}