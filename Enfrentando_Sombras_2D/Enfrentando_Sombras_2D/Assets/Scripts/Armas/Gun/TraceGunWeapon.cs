using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceGunWeapon : GunWeapon
{
    private AudioSource audioSource;

    [Header("Trace Gun Weapon")]
    public float range;

    public GameObject trailPrefab;

    private LineRenderer[] trails;


    private int trailIndex;

    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();

        base.Start();

        this.trails = new LineRenderer[this.bulletsPerShoot];

        for (int i = 0; i < this.bulletsPerShoot; i++)
        {
            GameObject go = Instantiate(this.trailPrefab);

            var line = go.GetComponent<LineRenderer>();
            line.enabled = false;

            this.trails[i] = line;

            go.transform.SetParent(this.transform);
        }

        this.trailIndex = 0;
    }

    protected override void ShootProjectile(Vector3 position, Vector3 direction)
    {
        audioSource.Play();

        RaycastHit2D hit = Physics2D.Raycast(position, direction, this.range);

        Vector3 shootStart = position;
        Vector3 shootEnd = position + direction * this.range;

        if (hit.collider != null)
        {
            shootEnd = hit.point;

            var health = hit.collider.GetComponent<Healt>();
            if (health != null)
            {
                this.OnHit(health, this.gameObject);
            }
        }

        StartCoroutine(this.DoTheTrail(shootStart, shootEnd, this.trailIndex));
        this.trailIndex++;
        this.trailIndex %= this.trails.Length;
    }


    IEnumerator DoTheTrail(Vector3 start, Vector3 end, int index)
    {
        var trail = this.trails[index];

        trail.enabled = true;

        trail.SetPosition(0, start);
        trail.SetPosition(1, end);

        yield return new WaitForSeconds(this.maxCooldownTime * 0.9f);

        trail.enabled = false;
    }
}