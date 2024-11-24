using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealtComponent : MonoBehaviour
{
    public int vida;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {
            var Player = other.gameObject.GetComponent<Healt>();
            if (Player != null)
            {
                bool vidaRecuperada = Player.RecuperarVida(vida);
                if (vidaRecuperada)
                {
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
