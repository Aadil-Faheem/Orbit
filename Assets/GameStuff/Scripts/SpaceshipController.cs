using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float thrustForce = 10f;
    public float rotationSpeed = 100f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Thrust
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrustForce, ForceMode.Acceleration);
        }

        // Rotation
        float yaw = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float pitch = Input.GetAxis("Vertical") * rotationSpeed * Time.deltaTime;
        transform.Rotate(pitch, yaw, 0);
    }
}