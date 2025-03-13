using UnityEngine;

public class PlanetCharacterController : MonoBehaviour
{
    public Transform planet;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevents unwanted rotations
    }

    void Update()
    {
        // Get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Move relative to the surface
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        rb.linearVelocity = moveDirection * moveSpeed;

        // Rotate character to align with planet surface
        Vector3 gravityDirection = (transform.position - planet.position).normalized;
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        // Apply planet gravity
        if (planet)
        {
            planet.GetComponent<PlanetGravity>().Attract(transform);
        }
    }
}
