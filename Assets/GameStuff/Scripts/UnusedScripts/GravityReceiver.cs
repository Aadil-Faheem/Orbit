using UnityEngine;

public class GravityReceiver : MonoBehaviour
{
    public float gravityMultiplier = 1f; // Adjust this for gameplay tuning
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable Unity's default gravity
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        CelestialGravity closestBody = FindClosestGravitySource();
        if (closestBody == null) return;

        Vector3 direction = (closestBody.transform.position - transform.position).normalized;
        float distanceSqr = (closestBody.transform.position - transform.position).sqrMagnitude;

        float gravityForce = (6.674f * Mathf.Pow(10, -11) * closestBody.mass) / distanceSqr;
        Vector3 gravityVector = direction * gravityForce * gravityMultiplier;

        rb.AddForce(gravityVector, ForceMode.Acceleration);
    }

    CelestialGravity FindClosestGravitySource()
    {
        CelestialGravity[] bodies = FindObjectsOfType<CelestialGravity>();
        CelestialGravity closest = null;
        float minDistanceSqr = float.MaxValue;

        foreach (CelestialGravity body in bodies)
        {
            float distanceSqr = (body.transform.position - transform.position).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                closest = body;
            }
        }

        return closest;
    }
}
