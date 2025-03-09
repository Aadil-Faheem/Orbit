using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float acceleration = 10f; // Acceleration force
    public float maxSpeed = 50f; // Maximum velocity
    public float rotationSpeed = 3f; // Mouse sensitivity
    public float rollSpeed = 50f; // Roll speed for Q/E

    private Vector3 velocity = Vector3.zero; // Current movement velocity
    private float pitch = 0f, yaw = 0f, roll = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 inputDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) inputDirection += transform.forward;  // Forward
        if (Input.GetKey(KeyCode.S)) inputDirection -= transform.forward;  // Backward
        if (Input.GetKey(KeyCode.A)) inputDirection -= transform.right;    // Left strafe
        if (Input.GetKey(KeyCode.D)) inputDirection += transform.right;    // Right strafe
        if (Input.GetKey(KeyCode.LeftShift)) inputDirection += transform.up;  // Up
        if (Input.GetKey(KeyCode.RightControl)) inputDirection -= transform.up; // Down

        // Apply acceleration with inertia
        velocity += inputDirection.normalized * acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        transform.position += velocity * Time.deltaTime;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Prevents flipping upside down

        // Handle Roll properly
        if (Input.GetKey(KeyCode.Q)) roll += rollSpeed * Time.deltaTime; // Roll left
        if (Input.GetKey(KeyCode.E)) roll -= rollSpeed * Time.deltaTime; // Roll right

        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
