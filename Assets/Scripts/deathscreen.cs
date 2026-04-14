using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using TMPro;


public class deathscreen : MonoBehaviour
{
    // ── References ────────────────────────────────────────────────
    [Header("References")]
    [SerializeField] private HealthManager HealthManager;

    [Tooltip("Full-screen black Image OUTSIDE attemptdeathscreen")]
    public Image fadePanel;

    [Tooltip("The attemptdeathscreen GameObject (starts inactive)")]
    public GameObject deathScreenUI;

    [Tooltip("Optional: Light2D on the Spotlight child object")]
    public Light2D spotlight;

    [Tooltip("The RectTransform that holds the bouncy letters (PauseFront)")]
    public RectTransform lettersContainer;

    [Tooltip("A TextMeshProUGUI prefab — one letter per instance")]
    public TextMeshProUGUI letterPrefab;

    [Tooltip("CanvasGroup on PauseFront — fades in the buttons at the end")]
    public CanvasGroup buttonsGroup;

    // ── Text Settings ─────────────────────────────────────────────
    [Header("Game Over Text")]
    public string gameOverText = "GAME OVER";
    public float letterSpacing = 60f;
    public float dropHeight = 400f;
    public float letterDelay = 0.1f;
    public float dropDuration = 0.45f;
    public float maxRandomTilt = 5f;

    [Tooltip("Bounce curve for letter landing. Add overshoot above 1 on Y for springiness.")]
    public AnimationCurve bounceCurve = new AnimationCurve(
        new Keyframe(0f, 0f, 0f, 8f),
        new Keyframe(0.55f, 1.1f, 0f, 0f),
        new Keyframe(0.75f, 0.9f, 0f, 0f),
        new Keyframe(0.9f, 1.05f, 0f, 0f),
        new Keyframe(1f, 1f, 0f, 0f)
    );

    // ── Spotlight Settings ────────────────────────────────────────
    [Header("Spotlight (optional)")]
    public float spotlightTargetIntensity = 1.5f;
    public float spotlightStartRadius = 0.5f;
    public float spotlightTargetRadius = 4f;
    public float spotlightDuration = 1.2f;

    // ── Timing ────────────────────────────────────────────────────
    [Header("Timing")]
    public float fadeDuration = 1.0f;
    public float delayAfterFade = 0.3f;
    public float delayAfterSpotlight = 0.4f;
    public float buttonFadeDuration = 0.5f;

    // ── Scene Names ───────────────────────────────────────────────
    [Header("Scenes")]
    public string gameSceneName = "LVL 1";
    public string menuSceneName = "VictorTest";

    // ── Internal ──────────────────────────────────────────────────
    private bool isDead = false;
    private List<RectTransform> spawnedLetters = new List<RectTransform>();

    void Start()
    {
        isDead = false;

        if (deathScreenUI != null) deathScreenUI.SetActive(false);
        if (fadePanel != null) SetImageAlpha(fadePanel, 0f);

        if (buttonsGroup != null)
        {
            buttonsGroup.alpha = 0f;
            buttonsGroup.interactable = false;
            buttonsGroup.blocksRaycasts = false;
        }

        if (spotlight != null)
        {
            spotlight.intensity = 0f;
            spotlight.pointLightOuterRadius = spotlightStartRadius;
        }
    }

    public void StartDeath()
    {
        if (isDead) return;
        isDead = true;
        StartCoroutine(DeathSequence());
    
}

    // ── Main Sequence ─────────────────────────────────────────────

    private IEnumerator DeathSequence()
    {
        // 1. Fade to black (game still running)
        yield return StartCoroutine(FadePanel(0f, 1f, fadeDuration));

        // 2. Freeze the game, show the death screen behind the black panel
        Time.timeScale = 0f;
        if (deathScreenUI != null) deathScreenUI.SetActive(true);

        yield return new WaitForSecondsRealtime(delayAfterFade);

        // 3. Fade black panel back out to reveal death screen
        yield return StartCoroutine(FadePanel(1f, 0f, fadeDuration));

        // 4. Fade in spotlight over the character
        if (spotlight != null)
            yield return StartCoroutine(AnimateSpotlight());

        yield return new WaitForSecondsRealtime(delayAfterSpotlight);

        // 5. Drop "GAME OVER" letters one by one
        yield return StartCoroutine(PlayLetters());

        // 6. Fade in buttons
        yield return StartCoroutine(FadeButtonsIn());
    }

    // ── Fade Panel ────────────────────────────────────────────────

    private IEnumerator FadePanel(float from, float to, float duration)
    {
        if (fadePanel == null) yield break;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            SetImageAlpha(fadePanel, Mathf.Lerp(from, to, t));
            yield return null;
        }
        SetImageAlpha(fadePanel, to);
    }

    // ── Spotlight ─────────────────────────────────────────────────

    private IEnumerator AnimateSpotlight()
    {
        float elapsed = 0f;
        while (elapsed < spotlightDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / spotlightDuration);
            spotlight.intensity = Mathf.Lerp(0f, spotlightTargetIntensity, t);
            spotlight.pointLightOuterRadius = Mathf.Lerp(spotlightStartRadius, spotlightTargetRadius, t);
            yield return null;
        }
        spotlight.intensity = spotlightTargetIntensity;
        spotlight.pointLightOuterRadius = spotlightTargetRadius;
    }

    // ── Bouncy Letters ────────────────────────────────────────────

    private IEnumerator PlayLetters()
    {
        if (letterPrefab == null || lettersContainer == null) yield break;

        // Clear old letters
        foreach (var old in spawnedLetters) if (old != null) Destroy(old.gameObject);
        spawnedLetters.Clear();

        char[] chars = gameOverText.ToCharArray();
        float totalWidth = (chars.Length - 1) * letterSpacing;
        float startX = -totalWidth / 2f;
        float currentX = startX;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ' ')
            {
                currentX += letterSpacing;
                continue;
            }

            TextMeshProUGUI letter = Instantiate(letterPrefab, lettersContainer);
            letter.text = chars[i].ToString();

            RectTransform rt = letter.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(currentX, 0f);
            rt.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-maxRandomTilt, maxRandomTilt));

            spawnedLetters.Add(rt);
            StartCoroutine(DropLetter(rt));

            currentX += letterSpacing;
            yield return new WaitForSecondsRealtime(letterDelay);
        }

        // Wait for last letter to finish
        yield return new WaitForSecondsRealtime(dropDuration);
    }

    private IEnumerator DropLetter(RectTransform rt)
    {
        if (rt == null) yield break;

        Vector2 finalPos = rt.anchoredPosition;
        Vector2 startPos = finalPos + new Vector2(0f, dropHeight);

        float elapsed = 0f;
        while (elapsed < dropDuration)
        {
            if (rt == null) yield break;
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / dropDuration);
            rt.anchoredPosition = Vector2.LerpUnclamped(startPos, finalPos, bounceCurve.Evaluate(t));
            yield return null;
        }

        if (rt != null) rt.anchoredPosition = finalPos;
    }

    // ── Buttons ───────────────────────────────────────────────────

    private IEnumerator FadeButtonsIn()
    {
        if (buttonsGroup == null) yield break;
        float elapsed = 0f;
        while (elapsed < buttonFadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            buttonsGroup.alpha = Mathf.Clamp01(elapsed / buttonFadeDuration);
            yield return null;
        }
        buttonsGroup.alpha = 1f;
        buttonsGroup.interactable = true;
        buttonsGroup.blocksRaycasts = true;
    }

    // ── Button Callbacks ──────────────────────────────────────────

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }

    // ── Helpers ───────────────────────────────────────────────────

    private void SetImageAlpha(Image image, float alpha)
    {
        if (image == null) return;
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }
}

