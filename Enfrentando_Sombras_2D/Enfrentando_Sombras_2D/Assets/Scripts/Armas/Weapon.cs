using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponItemPrefab;

    public float maxCooldownTime = 1f;
    public float cooldownTime;

    public float damage = 1;

    public bool isReady => this.cooldownTime > this.maxCooldownTime;
    protected void Awake()
    {
       this.cooldownTime = this.maxCooldownTime;
    }

    protected void Update()
    {
        if (this.isReady == false)
        {
            this.cooldownTime += Time.deltaTime;
        }
    }

    public void Active()
    {
        if (this.isReady)
        {
           
            this.OnActive();
            this.cooldownTime = 0;
        }

    }

   public virtual void OnHit(Healt healt) 
   {
        healt.Damage(this.damage, this.transform.position);
   }
    public virtual void Throw()
    {
        Vector3 offset = new Vector3(0, 1, 0); // Ajusta el offset en Y aquí según tu necesidad
        Vector3 spawnPosition = this.transform.position + offset; // Calcula la nueva posición

        Instantiate(this.weaponItemPrefab, spawnPosition,Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected abstract void OnActive();
}
