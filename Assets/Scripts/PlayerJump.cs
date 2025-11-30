using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpPower;
    [SerializeField] private float groundSpherePositionOffset;
    [SerializeField] private float groundSphereRadius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip doubleJumpClip;

    private Rigidbody _rb;
    private AudioSource _audioSource;
    private bool _canDoubleJump;

    public static bool IsGrounded { get; private set; }

    private void OnDrawGizmos()
    {
        // These two lines just to see the sphere in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.up * groundSpherePositionOffset, groundSphereRadius);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Create an invisible sphere in player position and modify the position up and down by multiplying groundSpherePositionOffset,
        //  then modify the sphere radius,
        //  sphere return true only when collide with specific layer mask
        IsGrounded = Physics.CheckSphere(transform.position + transform.up * groundSpherePositionOffset, groundSphereRadius, groundLayer);

        // If player is grounded > reset double jump
        if (IsGrounded) _canDoubleJump = true;

        // First jump: check if player pressed jump button and player is grounded
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            // Add force to the player in Y axis
            _rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            _audioSource.PlayOneShot(jumpClip);
        }

        // Second jump: check if player pressed jump button and player is not grounded and can double jump
        else if (Input.GetButtonDown("Jump") && !IsGrounded && _canDoubleJump)
        {
            // Add force to the player in Y axis, and disable double jump so he can only jump twice
            _rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            _audioSource.PlayOneShot(doubleJumpClip);
            _canDoubleJump = false;
        }
    }
}
