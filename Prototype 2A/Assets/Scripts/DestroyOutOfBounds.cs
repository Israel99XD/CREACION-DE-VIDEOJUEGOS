using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float _topBound = 30f;
    private float _lowerBound = -10f;


    private void FixedUpdate() 
    {
        if(transform.position.z > _topBound)
        {
            Destroy(gameObject);
        } else if(transform.position.z < _lowerBound)
        {
            Debug.Log("Game Over!");
            Destroy(gameObject);
        }
    }

}
