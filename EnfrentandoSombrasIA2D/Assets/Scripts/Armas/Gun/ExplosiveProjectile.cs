using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    [Header("Explosive Projectile")]
    public float explosionRange = 1f;

    public GameObject explosionPrefab;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
        Instantiate(this.explosionPrefab, this.transform.position, Quaternion.identity);

        Collider2D[] targets = Physics2D.OverlapCircleAll(this.transform.position, this.explosionRange);

        foreach (var target in targets)
        {
            Healt h = target.GetComponent<Healt>();
            if (h == null)
                continue;

            this.weapon.OnHit(h);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.explosionRange);
    }
}