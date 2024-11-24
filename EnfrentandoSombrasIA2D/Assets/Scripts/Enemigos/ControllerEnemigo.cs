using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControllerEnemigo : MonoBehaviour
{
    public GameObject prefab;
    public Transform[] spawnPoints;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var point in this.spawnPoints)
        {
            Instantiate(this.prefab, point.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}
