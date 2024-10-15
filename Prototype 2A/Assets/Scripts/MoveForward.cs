using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float Speed = 40f;

    private void FixedUpdate() 
    {
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * Speed);
    }
}
