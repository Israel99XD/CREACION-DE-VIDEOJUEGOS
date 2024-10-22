using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform Personaje;

    void Update()
    {
        if ( Personaje != null)
        {
            Vector3 position = transform.position;
            position.x = Personaje.position.x;
            transform.position = position;
        }
    }
}
