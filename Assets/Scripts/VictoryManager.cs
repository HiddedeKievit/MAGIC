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
    public string victoryText = "YOU WON!";
    public float letterSpacing = 70f;
    public float riseHeight = 400f;
    public float letterDelay = 0.08f;
    public float riseDuration = 0.45f;
    public float maxRandomTilt = 3f;

    [Header("Letter Size")]
    public float letterWidth = 180f;
    public float letterHeight = 300f;

    [Header("Letter Rainbow")]
    public bool useRainbowLetters = true;
    public float rainbowSpeed = 0.14f;
    public float rainbowSaturation = 0.45f;
    public float rainbowValue = 1f;
    public float rainbowOffsetPerLetter = 0.07f;

    [Header("Letter Hover After Pop")]
    public bool hoverLettersAfterPop = true;
    public float hoverSpeed = 2f;
    public float hoverAmount = 8f;
    public float hoverOffsetPerLetter = 0.25f;

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

    [Header("Particles")]
    public ParticleSystem victoryParticles;

    private bool victoryShown = false;

    private List<RectTransform> spawnedLetters = new List<RectTransform>();
    private List<TextMeshProUGUI> spawnedLetterTexts = new List<TextMeshProUGUI>();
    private List<Vector2> spawnedLetterBasePositions = new List<Vector2>();

    private Coroutine letterHoverRoutine;

    private void Start()
    {
        if (victoryScreen != null)
            victoryScreen.SetActive(false);

        if (flashPanel != null)
            SetAlpha(flashPanel, 0f);

        if (buttonsGroup != null)
        {
            buttonsGroup.alpha = 0f;
            buttonsGroup.interactable = false;
            buttonsGroup.blocksRaycasts = false;
        }

        if (victoryParticles != null)
        {
            ParticleSystem.MainModule main = victoryParticles.main;
            main.useUnscaledTime = true;

            victoryParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
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

        if (victoryScreen != null)
            victoryScreen.SetActive(true);

        // Set reward stars to empty immediately
        int starCount = GetStarCount(livesRemaining, maxLives);

        if (stars != null)
        {
            foreach (var s in stars)
            {
                if (s != null && starEmpty != null)
                {
                    s.sprite = starEmpty;
                    s.rectTransform.localScale = Vector3.zero;
                    s.rectTransform.localRotation = Quaternion.identity;
                }
            }
        }

        yield return new WaitForSecondsRealtime(delayAfterFlash);

        // 2. Flash back out
        if (flashPanel != null)
            yield return StartCoroutine(FadePanel(1f, 0f, flashDuration));

        // 3. Play confetti after flash is gone
        PlayVictoryParticles();

        // 4. Letters pop up one by one
        yield return StartCoroutine(PlayLetters());

        yield return new WaitForSecondsRealtime(delayAfterLetters);

        // 5. Stars spin in one by one
        yield return StartCoroutine(AnimateStars(starCount));

        // 6. Buttons fade in
        yield return StartCoroutine(FadeButtonsIn());
    }

    // ── Particles ─────────────────────────────────────────────────────

    private void PlayVictoryParticles()
    {
        if (victoryParticles == null)
        {
            Debug.LogWarning("Victory particles are not assigned in the VictoryManager Inspector.");
            return;
        }

        ParticleSystem.MainModule main = victoryParticles.main;
        main.useUnscaledTime = true;

        victoryParticles.gameObject.SetActive(true);
        victoryParticles.Clear(true);
        victoryParticles.Play(true);
    }

    // ── Letters ───────────────────────────────────────────────────────

    private IEnumerator PlayLetters()
    {
        if (letterPrefab == null || lettersContainer == null)
            yield break;

        if (letterHoverRoutine != null)
        {
            StopCoroutine(letterHoverRoutine);
            letterHoverRoutine = null;
        }

        foreach (var old in spawnedLetters)
        {
            if (old != null)
                Destroy(old.gameObject);
        }

        spawnedLetters.Clear();
        spawnedLetterTexts.Clear();
        spawnedLetterBasePositions.Clear();

        char[] chars = victoryText.ToCharArray();

        float totalWidth = 0f;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ' ')
                totalWidth += letterSpacing * 0.7f;
            else
                totalWidth += letterSpacing;
        }

        float currentX = -totalWidth / 2f;

        for (int i = 0; i < chars.Length; i++)
        {
            if (chars[i] == ' ')
            {
                currentX += letterSpacing * 0.7f;
                continue;
            }

            TextMeshProUGUI letter = Instantiate(letterPrefab, lettersContainer);

            // Important if the prefab is disabled in the hierarchy.
            letter.gameObject.SetActive(true);

            letter.text = chars[i].ToString();
            letter.alignment = TextAlignmentOptions.Center;
            letter.enableAutoSizing = false;

            RectTransform rt = letter.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(letterWidth, letterHeight);
            rt.anchoredPosition = new Vector2(currentX, 0f);
            rt.localRotation = Quaternion.Euler(0f, 0f, Random.Range(-maxRandomTilt, maxRandomTilt));
            rt.localScale = Vector3.one;

            // Set starting rainbow color immediately
            if (useRainbowLetters)
            {
                float hue = Mathf.Repeat(i * rainbowOffsetPerLetter, 1f);
                letter.color = Color.HSVToRGB(hue, rainbowSaturation, rainbowValue);
            }

            spawnedLetters.Add(rt);
            spawnedLetterTexts.Add(letter);

            StartCoroutine(RiseLetter(rt));

            currentX += letterSpacing;

            yield return new WaitForSecondsRealtime(letterDelay);
        }

        yield return new WaitForSecondsRealtime(riseDuration);

        // Save final positions after pop-in, then hover from those positions.
        spawnedLetterBasePositions.Clear();

        for (int i = 0; i < spawnedLetters.Count; i++)
        {
            if (spawnedLetters[i] != null)
                spawnedLetterBasePositions.Add(spawnedLetters[i].anchoredPosition);
            else
                spawnedLetterBasePositions.Add(Vector2.zero);
        }

        if (hoverLettersAfterPop || useRainbowLetters)
        {
            letterHoverRoutine = StartCoroutine(UpdateLettersAfterPop());
        }
    }

    private IEnumerator RiseLetter(RectTransform rt)
    {
        if (rt == null)
            yield break;

        Vector2 finalPos = rt.anchoredPosition;
        Vector2 startPos = finalPos - new Vector2(0f, riseHeight);

        float elapsed = 0f;

        while (elapsed < riseDuration)
        {
            if (rt == null)
                yield break;

            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / riseDuration);
            float bounced = bounceCurve.Evaluate(t);

            rt.anchoredPosition = Vector2.LerpUnclamped(startPos, finalPos, bounced);

            yield return null;
        }

        if (rt != null)
            rt.anchoredPosition = finalPos;
    }

    private IEnumerator UpdateLettersAfterPop()
    {
        while (true)
        {
            for (int i = 0; i < spawnedLetters.Count; i++)
            {
                RectTransform rt = spawnedLetters[i];

                if (rt == null)
                    continue;

                if (hoverLettersAfterPop && i < spawnedLetterBasePositions.Count)
                {
                    float offset = i * hoverOffsetPerLetter;
                    float y = Mathf.Sin((Time.unscaledTime * hoverSpeed) + offset) * hoverAmount;

                    rt.anchoredPosition = spawnedLetterBasePositions[i] + new Vector2(0f, y);
                }

                if (useRainbowLetters && i < spawnedLetterTexts.Count && spawnedLetterTexts[i] != null)
                {
                    float hue = Mathf.Repeat(Time.unscaledTime * rainbowSpeed + i * rainbowOffsetPerLetter, 1f);
                    spawnedLetterTexts[i].color = Color.HSVToRGB(hue, rainbowSaturation, rainbowValue);
                }
            }

            yield return null;
        }
    }

    // ── Reward Stars ──────────────────────────────────────────────────

    private IEnumerator AnimateStars(int count)
    {
        if (stars == null)
            yield break;

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == null)
                continue;

            bool filled = i < count;

            yield return StartCoroutine(SpinStarIn(stars[i], filled));
            yield return new WaitForSecondsRealtime(starDelay);
        }
    }

    private IEnumerator SpinStarIn(Image star, bool filled)
    {
        RectTransform rt = star.GetComponent<RectTransform>();

        if (rt == null)
            yield break;

        star.sprite = filled ? starFilled : starEmpty;

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
    }

    // ── Buttons ───────────────────────────────────────────────────────

    private IEnumerator FadeButtonsIn()
    {
        if (buttonsGroup == null)
            yield break;

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

    private int GetStarCount(int livesRemaining, int maxLives)
    {
        if (maxLives <= 0)
            return 1;

        float ratio = (float)livesRemaining / maxLives;

        if (ratio >= 1f)
            return 3;
        else if (ratio >= 0.5f)
            return 2;
        else
            return 1;
    }

    private IEnumerator FadePanel(float from, float to, float duration)
    {
        if (flashPanel == null)
            yield break;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / duration);
            SetAlpha(flashPanel, Mathf.Lerp(from, to, t));

            yield return null;
        }

        SetAlpha(flashPanel, to);
    }

    private void SetAlpha(Image image, float alpha)
    {
        if (image == null)
            return;

        Color c = image.color;
        c.a = alpha;
        image.color = c;
    }

    // ── Button Callbacks ──────────────────────────────────────────────

    public void OnNextLevel()
    {
        Time.timeScale = 1f;

        int next = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene("BramTest");
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("VictorTest");
    }
}
