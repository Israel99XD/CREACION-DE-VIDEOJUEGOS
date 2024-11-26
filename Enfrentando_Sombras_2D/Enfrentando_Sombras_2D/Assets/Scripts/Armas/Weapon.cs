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

   public virtual void OnHit(Healt healt, GameObject instigator)
    {
        // Si tanto el instigator como el objeto afectado tienen el Tag "Enemigo", no aplica da�o
        if (instigator.CompareTag("Enemigo") && this.gameObject.CompareTag("Enemigo"))
        {
            return; // Sal del m�todo sin aplicar da�o
        }
        healt.Damage(this.damage, this.transform.position);
   }
    public virtual void Throw()
    {
        Vector3 offset = new Vector3(0, 2, 0); // Ajusta el offset en Y aqu� seg�n tu necesidad
        Vector3 spawnPosition = this.transform.position + offset; // Calcula la nueva posici�n

        Instantiate(this.weaponItemPrefab, spawnPosition,Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected abstract void OnActive();
}
