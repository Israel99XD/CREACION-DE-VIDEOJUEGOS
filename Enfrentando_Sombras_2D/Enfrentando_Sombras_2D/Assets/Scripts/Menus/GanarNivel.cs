using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GanarNivel : MonoBehaviour
{
    public GameObject menuContenedor;
    public int nivel;
    public int personaje;
    private Menu menu;

    private void Start()
    {
        menu = menuContenedor.GetComponent<Menu>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var Player = other.gameObject.GetComponent<Healt>();
            if (Player != null)
            {
                menu.PasarNivel(nivel,personaje);
            }
        }

    }
}
