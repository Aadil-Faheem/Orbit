using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float speedMultiplier = 5f; // How much to speed up time
    private bool isFastForward = false;
    private float normalSpeed = 1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isFastForward)
            {
                Time.timeScale = normalSpeed; // Reset to normal speed
            }
            else
            {
                Time.timeScale = speedMultiplier; // Speed up time
            }

            isFastForward = !isFastForward; // Toggle state
        }
    }
}
