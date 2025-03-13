using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravityForce = 9.8f;

    public void Attract(Transform body)
    {
        Vector3 gravityDirection = (transform.position - body.position).normalized;
        body.GetComponent<Rigidbody>().AddForce(gravityDirection * gravityForce, ForceMode.Acceleration);
    }
}
