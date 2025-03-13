using UnityEngine;

public class PlayerSwitchController : MonoBehaviour
{
    public GameObject fpsController; // Assign FPS Controller in Inspector
    public GameObject spaceship; // Assign spaceship in Inspector
    public Transform spaceshipSeat; // Cockpit seat position

    private bool isInShip = false;
    private Rigidbody shipRb;
    private SpaceshipController shipController;
    private AudioListener fpsAudioListener; // Reference to FPS Audio Listener

    private void Start()
    {
        shipRb = spaceship.GetComponent<Rigidbody>();
        shipController = spaceship.GetComponent<SpaceshipController>();
        fpsAudioListener = fpsController.GetComponentInChildren<AudioListener>();

        // Ensure game starts in FPS mode
        fpsController.SetActive(true);
        spaceship.SetActive(true);
        shipController.enabled = false;
        if (fpsAudioListener) fpsAudioListener.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isInShip)
            {
                ExitSpaceship();
            }
            else if (Vector3.Distance(transform.position, spaceship.transform.position) < 5f)
            {
                EnterSpaceship();
            }
        }
    }

    private void EnterSpaceship()
    {
        isInShip = true;

        // Disable FPS Controller & Audio Listener
        fpsController.SetActive(false);
        if (fpsAudioListener) fpsAudioListener.enabled = false;

        // Move player to cockpit
        transform.position = spaceshipSeat.position;
        transform.SetParent(spaceship.transform);

        // Enable spaceship controls
        shipController.enabled = true;
    }

    private void ExitSpaceship()
    {
        isInShip = false;

        // Enable FPS Controller & Audio Listener
        fpsController.SetActive(true);
        if (fpsAudioListener) fpsAudioListener.enabled = true;

        // Detach from spaceship and place back on ground
        transform.SetParent(null);
        transform.position = spaceship.transform.position + Vector3.down * 2f;

        // Disable spaceship controls
        shipController.enabled = false;
    }
}
