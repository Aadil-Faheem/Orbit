using PlayerLogic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    /**
     * Ui element that fades the screen, which simulates the player death
     */
    [RequireComponent(typeof(Image))]
    public class DeathScreen : MonoBehaviour
    {
        private const float FadingSpeed = 0.005f;
        private const float deathResetDelay = 2.5f; // ⏱️ Seconds to wait before reload

        private Image deathBlackFadeImage;
        [SerializeField] private Player player;

        [SerializeField] private Collider sunCollider;
        [SerializeField] private TMPro.TextMeshProUGUI deathMessageText;

        private new bool enabled;
        private bool hasTriggeredReset = false;

        public void Awake()
        {
            deathBlackFadeImage = GetComponent<Image>();
            if (deathMessageText != null)
                deathMessageText.enabled = false;
        }

        public void Start()
        {
            player.Dieable.OnDeath += Enable;
        }

        public void OnDestroy()
        {
            player.Dieable.OnDeath -= Enable;
        }

        public void Update()
        {
            if (!enabled)
            {
                CheckSunCollision();
                return;
            }

            FadeScreen();
        }

        private void Enable()
        {
            enabled = true;
            if (deathMessageText != null)
            {
                deathMessageText.text = "You are Dead";
                deathMessageText.enabled = true;
            }
        }

        private void FadeScreen()
        {
            Color nextColor = deathBlackFadeImage.color;
            nextColor.a += FadingSpeed;
            deathBlackFadeImage.color = nextColor;

            if (nextColor.a >= 1f && !hasTriggeredReset)
            {
                hasTriggeredReset = true;
                Invoke(nameof(ReloadLevel), deathResetDelay); // ⏱️ Delay before reload
            }
        }

        private void ReloadLevel()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        private void CheckSunCollision()
        {
            if (sunCollider != null && sunCollider.bounds.Intersects(player.GetComponent<Collider>().bounds))
            {
                player.Dieable.Die();
            }
        }
    }
}
