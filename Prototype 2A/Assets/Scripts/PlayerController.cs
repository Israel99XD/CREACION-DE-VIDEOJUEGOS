using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerActions _playerActions; //Input System
    private Vector2 _moveInput; //WASD Movement

    public float Speed = 10f;
    public float XRange = 10f;

    public GameObject ProjectilePrefab;

    private void Awake() 
    {
        _playerActions = new PlayerActions();
    }

    private void Start()
    {
        _playerActions.Enable();
        _playerActions.PlayerController.Shoot.performed += ctx => Shoot();
    }

    // Update is called once per frame
    private void Update()
    {
        CaptureInput();
    }

    private void FixedUpdate() 
    {
        if(transform.position.x < -XRange)
        {
            transform.position = new Vector3(-XRange, transform.position.y, transform.position.z);
        } else if(transform.position.x > XRange)
        {
            transform.position = new Vector3(XRange, transform.position.y, transform.position.z);
        } else {
            transform.Translate(Vector3.right * _moveInput.x * Time.fixedDeltaTime * Speed);
        }
    }

    private void Shoot()
    {
        Instantiate(ProjectilePrefab, transform.position, ProjectilePrefab.transform.rotation);
    }

    private void CaptureInput()
    {
        _moveInput = _playerActions.PlayerController.Movement.ReadValue<Vector2>();
    }
}
