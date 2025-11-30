using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioSource footstepAudioSource;
    [SerializeField] private AudioClip grassFootstepClip;
    [SerializeField] private float movementSpeed;

    private Rigidbody _rb;
    private float _horizontalInput;
    private float _verticalInput;
    private Vector3 _movementDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Start the coroutine of PlayFootstep
        StartCoroutine(PlayFootstep());
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

    private IEnumerator PlayFootstep()
    {
        // While true and never stops till the game stop
        while (true)
        {
            // Check if player movement input not equal zero and player is grounded
            if (_movementDirection != Vector3.zero && PlayerJump.IsGrounded)
            {
                // Use the reference of audio source and play grass footstep audio clip
                footstepAudioSource.PlayOneShot(grassFootstepClip);

                // Wait 0.7f seconds and then continue the loop
                yield return new WaitForSeconds(0.7f);
            }

            // This line to ensure while loop stop in one frame, without using this line the game will crash
            yield return null;
        }
    }
}
