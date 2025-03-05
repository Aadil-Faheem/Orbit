using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class OrbitVisualizer : MonoBehaviour
{
    public int numPoints = 100; // Number of points in the trajectory
    public float timeStep = 0.1f; // Time step for simulation
    public PlanetGravity planet;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numPoints;
    }

    void Update()
    {
        DrawOrbit();
    }

    void DrawOrbit()
    {
        Vector3[] points = new Vector3[numPoints];
        Vector3 currentPosition = transform.position;
        Vector3 velocity = GetComponent<Rigidbody>().linearVelocity;

        for (int i = 0; i < numPoints; i++)
        {
            Vector3 directionToPlanet = (planet.transform.position - currentPosition).normalized;
            float distance = Vector3.Distance(planet.transform.position, currentPosition);
            float gravityForce = planet.gravityStrength / (distance * distance);
            Vector3 acceleration = directionToPlanet * gravityForce;

            velocity += acceleration * timeStep;
            currentPosition += velocity * timeStep;
            points[i] = currentPosition;
        }

        lineRenderer.SetPositions(points);
    }
}
