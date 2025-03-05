using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravityStrength = 100f; // Increased for better orbital effect

    void FixedUpdate()
    {
        ApplyGravityToObjects();
    }

    void ApplyGravityToObjects()
    {
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, 100f);

        foreach (Collider col in affectedObjects)
        {
            Rigidbody rb = col.attachedRigidbody;
            if (rb != null && rb != GetComponent<Rigidbody>())
            {
                Vector3 directionToCenter = (transform.position - rb.position).normalized;
                float distance = Vector3.Distance(transform.position, rb.position);
                float gravityForce = gravityStrength / (distance * distance); // Inverse square law

                rb.AddForce(directionToCenter * gravityForce * rb.mass, ForceMode.Acceleration);
            }
        }
    }
}
