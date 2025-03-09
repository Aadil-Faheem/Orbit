using UnityEngine;
using System.Collections.Generic;

public class CelestialGravity : MonoBehaviour
{
    public float mass = 1000f; // Mass of the celestial body
    public Vector3 initialVelocity; // This is now public
    private Vector3 velocity;

    private static List<CelestialGravity> allBodies = new List<CelestialGravity>();

    private void OnEnable()
    {
        allBodies.Add(this);
        velocity = initialVelocity; // Assign initial velocity for orbiting
    }

    private void OnDisable()
    {
        allBodies.Remove(this);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        transform.position += velocity * Time.fixedDeltaTime;
    }

    void ApplyGravity()
    {
        foreach (CelestialGravity other in allBodies)
        {
            if (other == this) continue;

            Vector3 direction = other.transform.position - transform.position;
            float distanceSqr = direction.sqrMagnitude;
            if (distanceSqr < 0.1f) continue; // Prevent division by zero

            float forceMagnitude = (6.674f * Mathf.Pow(10, -11) * mass * other.mass) / distanceSqr;
            Vector3 acceleration = direction.normalized * forceMagnitude / mass;

            velocity += acceleration * Time.fixedDeltaTime;
        }
    }
}
