using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get horizontal input which is A/D
        // Get vertical input which is W/S
        // I did these lines in Update method to make the game take input in each frame and not be delayed
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        // transform.right is Vector3(1, 0, 0), transform.forward is Vector3(0, 0, 1). These two vectors in local axis of player not world global axis
        // multiply right by horizontal input to make player move right and left
        // multiply forward by vertical input to make player move forward and backward
        // add both vectors and then multiply it by player movement speed
        _movementDirection = (transform.right * _horizontalInput + transform.forward * _verticalInput) * movementSpeed;

        // change the rigidbody velocity of player to the new movement direction, and leave Y axis same to let Unity handle the gravity;
        _rb.linearVelocity = new Vector3(_movementDirection.x, _rb.linearVelocity.y, _movementDirection.z);
    }
}
