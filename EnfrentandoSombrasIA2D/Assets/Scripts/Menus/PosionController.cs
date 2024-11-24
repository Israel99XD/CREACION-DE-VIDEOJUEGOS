using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PosionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ControllerUser.Instance.SetPosion(true);
            Destroy(this.gameObject);
        }

    }
}
