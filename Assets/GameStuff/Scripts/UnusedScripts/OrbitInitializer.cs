using UnityEngine;

public class OrbitInitializer : MonoBehaviour
{
    public CelestialGravity orbitingBody; // The planet or moon
    public CelestialGravity centralBody; // The Sun or parent planet

    private void Start()
    {
        if (orbitingBody == null || centralBody == null) return;

        float G = 6.674f * Mathf.Pow(10, -11); // Gravitational constant
        float r = Vector3.Distance(orbitingBody.transform.position, centralBody.transform.position);
        float velocityMagnitude = Mathf.Sqrt(G * centralBody.mass / r); // Orbital velocity formula

        Vector3 direction = (orbitingBody.transform.position - centralBody.transform.position).normalized;
        Vector3 orbitalVelocity = Vector3.Cross(direction, Vector3.up) * velocityMagnitude; // Perpendicular velocity

        orbitingBody.initialVelocity = orbitalVelocity; // Assign proper orbit speed
    }
}
