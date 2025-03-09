using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    public Transform orbitCenter; // The object this planet orbits (e.g., the Sun)
    public float orbitRadius = 10f; // Distance from orbit center
    public float orbitSpeed = 10f; // Speed of orbit (degrees per second)

    private float currentAngle = 0f;

    private void Update()
    {
        if (orbitCenter == null) return;

        currentAngle += orbitSpeed * Time.deltaTime; // Increase angle over time
        float angleRad = currentAngle * Mathf.Deg2Rad; // Convert degrees to radians

        // Calculate new position
        float x = Mathf.Cos(angleRad) * orbitRadius;
        float z = Mathf.Sin(angleRad) * orbitRadius;
        transform.position = orbitCenter.position + new Vector3(x, 0, z);
    }
}
