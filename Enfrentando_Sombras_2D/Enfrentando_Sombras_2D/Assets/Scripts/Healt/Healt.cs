using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class Healt : MonoBehaviour
{
    public float maxHealth;

    public float percent
    {
        get { return this.currentHealth / this.maxHealth; }
    }

    protected float currentHealth;

    public bool isPlayer;
    public int puntaje;

    private static int score = 0;
    public Action<float, Vector3> OnDamage;
    public Action OnDeath;
    protected virtual void Awake()
    {
        ControllerUser.Instance.SetScoreA(0);
        this.currentHealth = this.maxHealth;
    }

    public virtual void Restore(float amount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth + amount, 0, this.maxHealth);
    }

    public virtual void Damage(float amount, Vector3 instigatorLocation)
    {
        if (this.OnDamage != null)
        {
            this.OnDamage(amount, instigatorLocation);
        }
        this.currentHealth = Mathf.Clamp(this.currentHealth - amount, 0, this.maxHealth);

        if (this.currentHealth == 0)
        {
            this.Die();
        }
    }

    public virtual void Die()
    {
        
        if (isPlayer)
        {
            score = 0;
            this.gameObject.SetActive(false);
        }
        else
        {
            score += puntaje;
            Destroy(this.gameObject);
        }

        ControllerUser.Instance.SumarPuntos(score);
        if (this.OnDeath != null)
        {
            this.OnDeath();
        }
        Debug.Log("Score: " + score);
    }

    public float GetHealthPercent()
    {
        return currentHealth / maxHealth;
    }

    public bool RecuperarVida(float amount)
    {
        if (this.currentHealth < this.maxHealth)
        {
            this.currentHealth = Mathf.Clamp(this.currentHealth + amount, 0, this.maxHealth);
            return true;
        }

        return false;
    }
    public static int ObtenerPuntaje()
    {
        return score;
    }

    public void Reiniciar()
    {
        score = 0;
        Time.timeScale = 1f;
        ControllerUser.Instance.SumarPuntos(score);
    }
}

