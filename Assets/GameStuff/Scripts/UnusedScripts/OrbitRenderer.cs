using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRenderer : MonoBehaviour
{
    public Transform orbitCenter; // The Sun or parent planet
    public int segments = 100; // Number of points in the orbit
    public float orbitRadius = 10f; // Distance from orbit center
    public Color orbitColor = Color.white; // Line color

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Basic material
        lineRenderer.startColor = orbitColor;
        lineRenderer.endColor = orbitColor;

        DrawOrbit();
    }

    void DrawOrbit()
    {
        Vector3[] points = new Vector3[segments + 1];

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * (360f / segments) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * orbitRadius;
            float z = Mathf.Sin(angle) * orbitRadius;
            points[i] = new Vector3(x, 0, z) + orbitCenter.position;
        }

        lineRenderer.SetPositions(points);
    }
}
