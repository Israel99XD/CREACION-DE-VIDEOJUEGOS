using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GunWeapon : Weapon
{
    [Header("Gun")]
    public GunType type;
    public int maxAmmo;
    private int _currentAmmo;
    public int currentAmmo
    {
        get => this._currentAmmo;
        set
        {
            this._currentAmmo = Mathf.Clamp(value, 0, this.maxAmmo);

            if (this.ui != null)
                this.ui.SetAmmo(this._currentAmmo, this.maxAmmo);
        }
    }

    public bool isAmmoAtMax
    {
        get => this._currentAmmo == this.maxAmmo;
    }
    public int bulletsPerShoot = 1;

    public Transform shootPoint;

    [Header("Shoot spread")]
    public float maxDispersionAmount = 0;
    public Transform dispersionPoint;

    [HideInInspector]
    public AmmoUI ui;
    protected virtual void Start()
    {
    }


    protected override void OnActive()
    {
        if (this.currentAmmo == 0)
            return;

        this.currentAmmo--;

        for (int i = 0; i < this.bulletsPerShoot; i++)
        {
            // bum bum

            float dispersionAmount = Random.Range(-this.maxDispersionAmount, this.maxDispersionAmount);

            Vector3 dispersionDirection = this.dispersionPoint.up * dispersionAmount;
            Vector3 dispersionTarget = this.dispersionPoint.position + dispersionDirection;

            Vector3 shootDirection = (dispersionTarget - this.shootPoint.position).normalized;

            this.ShootProjectile(this.shootPoint.position, shootDirection);
        }
    }

    public override void Throw()
    {

        Vector3 offset = new Vector3(0, 1, 0); // Ajusta el offset en Y aquí según tu necesidad
        Vector3 spawnPosition = this.transform.position + offset; // Calcula la nueva posición

        var go = Instantiate(this.weaponItemPrefab, spawnPosition, Quaternion.identity);

        var item = go.GetComponent<WeaponItem>();
        item.startingAmmo = this._currentAmmo;

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        // Draw dispersion bubble
        Gizmos.DrawWireSphere(this.dispersionPoint.position, this.maxDispersionAmount);
    }

    protected abstract void ShootProjectile(Vector3 position, Vector3 direction);
}
