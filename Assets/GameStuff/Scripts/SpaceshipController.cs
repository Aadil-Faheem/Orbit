using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float thrustPower = 10f;
    public float rotationSpeed = 2f;
    public Rigidbody rb;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleRotation();
    }

    void FixedUpdate()
    {
        HandleThrust();
    }

    void HandleThrust()
    {
        Vector3 thrustDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift)) thrustDirection += transform.forward;
        if (Input.GetKey(KeyCode.LeftControl)) thrustDirection -= transform.forward;
        if (Input.GetKey(KeyCode.W)) thrustDirection += transform.up;
        if (Input.GetKey(KeyCode.S)) thrustDirection -= transform.up;
        if (Input.GetKey(KeyCode.A)) thrustDirection -= transform.right;
        if (Input.GetKey(KeyCode.D)) thrustDirection += transform.right;

        rb.AddForce(thrustDirection * thrustPower, ForceMode.Force);
    }

    void HandleRotation()
    {
        float yaw = Input.GetAxis("Mouse X") * rotationSpeed;
        float pitch = -Input.GetAxis("Mouse Y") * rotationSpeed;
        float roll = 0f;

        if (Input.GetKey(KeyCode.A)) roll = 1f;
        if (Input.GetKey(KeyCode.D)) roll = -1f;

        transform.Rotate(pitch, yaw, roll * rotationSpeed, Space.Self);
    }
}
