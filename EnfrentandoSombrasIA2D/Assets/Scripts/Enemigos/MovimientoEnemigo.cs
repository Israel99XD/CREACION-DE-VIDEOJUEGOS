using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float distancia;
    [SerializeField] private LayerMask queEsSuelo;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
       Rigidbody2D = GetComponent<Rigidbody2D>();
       boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D.velocity = new Vector2(velocidadMovimiento * transform.right.x, Rigidbody2D.velocity.y);

        Vector2 puntoOrigen = transform.position + (boxCollider.size.x / 2f) * transform.right;


        RaycastHit2D informacionSuelo = Physics2D.Raycast(puntoOrigen, transform.right, distancia, queEsSuelo);

        if (informacionSuelo)
        {
            Girar();
        }
    }

    private void Girar()
    {
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (boxCollider.size.x / 2f) * transform.right, transform.position + transform.right * distancia);
    }
}
