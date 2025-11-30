using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float sensetivity;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation;

    private void Start()
    {
        // Hide the cursor when the game start
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get the input from mouse and multiply it by sensetivity, get mouse inputs in Update method each frame and not be delayed
        _mouseX = Input.GetAxis("Mouse X") * sensetivity;
        _mouseY = Input.GetAxis("Mouse Y") * sensetivity;
    }

    private void LateUpdate()
    {
        // Subtract mouse y value from xRotation and clamp it to limit the camera from rotating vertically more than -90, 90 degree
        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Rotate the camera only vertically
        mainCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Rotate the player object horizontally, camera object is a child of player object, so it will rotate horizontally with player
        transform.Rotate(transform.up * _mouseX);
    }
}
