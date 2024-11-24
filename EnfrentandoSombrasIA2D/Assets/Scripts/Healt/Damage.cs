using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var playerHealth = other.GetComponent<Healt>();
            if (playerHealth != null)
            {
                playerHealth.Damage(999, this.transform.position);
            }
        }
    }
}
