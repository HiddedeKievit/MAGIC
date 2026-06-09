using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    [Header("References")]
    public GameObject victoryScreen;
    public Image flashPanel;
    public CanvasGroup buttonsGroup;
    public RectTransform lettersContainer;
    public TextMeshProUGUI letterPrefab;
    public RectTransform starsContainer;

    [Header("Victory Text")]
    public string victoryText = "VICTORY";
    public float letterSpacing = 70f;
    public float riseHeight = 400f;
    public float letterDelay = 0.08f;
    public float riseDuration = 0.45f;
    public float maxRandomTilt = 6f;

    [Tooltip("Bounce curve — overshoot above 1 gives springiness")]
    public AnimationCurve bounceCurve = new AnimationCurve(
        new Keyframe(0f, 0f, 0f, 8f),
        new Keyframe(0.55f, 1.1f, 0f, 0f),
        new Keyframe(0.75f, 0.9f, 0f, 0f),
        new Keyframe(0.9f, 1.05f, 0f, 0f),
        new Keyframe(1f, 1f, 0f, 0f)
    );

    [Header("Stars")]
    public Image[] stars;
    public Sprite starFilled;
    public Sprite starEmpty;
    public float starDelay = 0.2f;
    public float starSpinDuration = 0.4f;

    [Header("Timing")]
    public float flashDuration = 0.4f;
    public float delayAfterFlash = 0.2f;
    public float delayAfterLetters = 0.3f;
    public float buttonFadeDuration = 0.5f;

    [Header("Optional")]
    public ParticleSystem victoryParticles;

    private bool victoryShown = false;
    private List<RectTransform> spawnedLetters = new List<RectTransform>();

    void Start()
    {
        if (victoryScreen != null) victoryScreen.SetActive(false);
        if (flashPanel != null) SetAlpha(flashPanel, 0f);
        if (buttonsGroup != null)
        {
            buttonsGroup.alpha = 0f;
            buttonsGroup.interactable = false;
            buttonsGroup.blocksRaycasts = false;
        }
    }

    public void TriggerVictory(int livesRemaining, int maxLives)
    {
        if (victoryShown) return;
        victoryShown = true;
        StartCoroutine(VictorySequence(livesRemaining, maxLives));
    }

    private IEnumerator VictorySequence(int livesRemaining, int maxLives)
    {
        // 1. Flash white
        if (flashPanel != null)
            yield return StartCoroutine(FadePanel(0f, 1f, flashDuration * 0.3f));

        Time.timeScale = 0f;
        if (victoryScreen != null) victoryScreen.SetActive(true);
        if (victoryParticles != null) victoryParticles.Play();

        // Set stars to empty immediately (they'll animate in later)
        int starCount = GetStarCount(livesRemaining, maxLives);
        if (stars != null)
            foreach (var s in stars)
                if (s != null && starEmpty != null) s.sprite = starEmpty;

        yield return new WaitForSecondsRealtime(delayAfterFlash);

        // 2. Flash back out
        if (flashPanel != null)
            yield return StartCoroutine(FadePanel(1f, 0f, flashDuration));

        // 3. Letters pop up one by one
        yield return StartCoroutine(PlayLetters());

        yield return new WaitForSecondsRealtime(delayAfterLetters);

        // 4. Stars spin in one by one
        yield return StartCoroutine(AnimateStars(starCount));

        // 5. Buttons fade in
        yield return StartCoroutine(FadeButtonsIn());
    }

    // ── Letters ───────────────────────────────────────────────────────

    private IEnumerator PlayLetters()
    {
        if (letterPrefab == null || lettersContainer == null) yield break;

        foreach (var old in spawnedLetters) if (old != null) Destroy(old.gameObject);
        spawnedLetters.Clear();

        char[] chars = victoryText.ToCharArray();
        float totalWidth = (chars.Length - 1) * letterSpacing;
        float startX = -totalWidth / 2f;
        float currentX = startX;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ' ') { currentX += letterSpacing; continue; }

            TextMeshProUGUI letter = Instantiate(letterPrefab, lettersContainer);
            letter.text = chars[i].ToString();

            RectTransform rt = letter.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(currentX, 0f);
            rt.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-maxRandomTilt, maxRandomTilt));

            spawnedLetters.Add(rt);
            StartCoroutine(RiseLetter(rt));

            currentX += letterSpacing;
            yield return new WaitForSecondsRealtime(letterDelay);
        }

        yield return new WaitForSecondsRealtime(riseDuration);
    }

    private IEnumerator RiseLetter(RectTransform rt)
    {
        if (rt == null) yield break;

        Vector2 finalPos = rt.anchoredPosition;
        Vector2 startPos = finalPos - new Vector2(0f, riseHeight); // rise from below

        float elapsed = 0f;
        while (elapsed < riseDuration)
        {
            if (rt == null) yield break;
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / riseDuration);
            rt.anchoredPosition = Vector2.LerpUnclamped(startPos, finalPos, bounceCurve.Evaluate(t));
            yield return null;
        }

        if (rt != null) rt.anchoredPosition = finalPos;
    }

    // ── Stars ─────────────────────────────────────────────────────────

    private IEnumerator AnimateStars(int count)
    {
        if (stars == null) yield break;

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == null) continue;
            bool filled = i < count;
            yield return StartCoroutine(SpinStarIn(stars[i], filled));
            yield return new WaitForSecondsRealtime(starDelay);
        }
    }

    private IEnumerator SpinStarIn(Image star, bool filled)
    {
        RectTransform rt = star.GetComponent<RectTransform>();
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < starSpinDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / starSpinDuration);
            float bounced = bounceCurve.Evaluate(t);
            rt.localScale = Vector3.LerpUnclamped(startScale, endScale, bounced);
            rt.localRotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(180f, 0f, t));
            yield return null;
        }

        rt.localScale = endScale;
        rt.localRotation = Quaternion.identity;
        star.sprite = filled ? starFilled : starEmpty;
    }

    // ── Buttons ───────────────────────────────────────────────────────

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

    // ── Helpers ───────────────────────────────────────────────────────

    int GetStarCount(int livesRemaining, int maxLives)
    {
        float ratio = (float)livesRemaining / maxLives;
        if (ratio >= 1f) return 3;
        else if (ratio >= 0.5f) return 2;
        else return 1;
    }

    private IEnumerator FadePanel(float from, float to, float duration)
    {
        if (flashPanel == null) yield break;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            SetAlpha(flashPanel, Mathf.Lerp(from, to, Mathf.Clamp01(elapsed / duration)));
            yield return null;
        }
        SetAlpha(flashPanel, to);
    }

    private void SetAlpha(Image image, float alpha)
    {
        if (image == null) return;
        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }

    // ── Button Callbacks ──────────────────────────────────────────────

    public void OnNextLevel()
    {
        Time.timeScale = 1f;
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(next);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScreen");
    }
}
