using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text regionName;
    public Image lineTop;
    public Image lineBottom;

    [Header("Settings")]
    public float lineFullWidth = 400f;
    public float fadeInDuration = 0.6f;
    public float holdDuration = 2.0f;
    public float fadeOutDuration = 0.5f;

    [Header("Vertical Offset")]
    [Tooltip("How many pixels the text rises during fade-in")]
    public float slideDistance = 15f;

    [Header("Auto Show on Start")]
    public bool showOnStart = false;
    public string levelName = "The Forest";

    private Coroutine activeCoroutine;
    private RectTransform textRect;
    private float textStartY;

    void Start()
    {
        textRect = regionName.rectTransform;
        textStartY = textRect.anchoredPosition.y;

        SetAlpha(regionName, 0f);
        SetAlpha(lineTop, 0f);
        SetAlpha(lineBottom, 0f);
        SetLineWidth(0f);

        if (showOnStart)
            Show(levelName);
    }

    // Call this from any script: FindObjectOfType<Fader>().Show("Level Name");
    public void Show(string name)
    {
        regionName.text = name;
        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
        activeCoroutine = StartCoroutine(PlayPopup());
    }

    // Called when the player walks into a collider tagged "Region"
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Region"))
        {
            Show(other.name);
        }
    }

    IEnumerator PlayPopup()
    {
        // --- Reset ---
        SetAlpha(regionName, 0f);
        SetAlpha(lineTop, 0f);
        SetAlpha(lineBottom, 0f);
        SetLineWidth(0f);
        SetTextY(textStartY - slideDistance);

        // --- Fade in: lines expand + text slides up ---
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeInDuration);
            float e = EaseOut(t);

            SetLineWidth(e * lineFullWidth);
            SetAlpha(lineTop, e);
            SetAlpha(lineBottom, e);
            SetAlpha(regionName, e);

            float y = Mathf.Lerp(textStartY - slideDistance, textStartY, e);
            SetTextY(y);

            yield return null;
        }

        // Fully visible at final position
        SetAlpha(regionName, 1f);
        SetAlpha(lineTop, 1f);
        SetAlpha(lineBottom, 1f);
        SetLineWidth(lineFullWidth);
        SetTextY(textStartY);

        // --- Hold ---
        yield return new WaitForSeconds(holdDuration);

        // --- Fade out (text stays in place, just fades) ---
        elapsed = 0f;
        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeOutDuration);
            float inv = 1f - EaseIn(t);

            SetAlpha(regionName, inv);
            SetAlpha(lineTop, inv);
            SetAlpha(lineBottom, inv);

            yield return null;
        }

        SetAlpha(regionName, 0f);
        SetAlpha(lineTop, 0f);
        SetAlpha(lineBottom, 0f);
        SetLineWidth(0f);
        SetTextY(textStartY);
        activeCoroutine = null;
    }

    // ── Helpers ──────────────────────────────────────────────────────

    void SetLineWidth(float width)
    {
        if (lineTop)
        {
            var s = lineTop.rectTransform.sizeDelta;
            lineTop.rectTransform.sizeDelta = new Vector2(width, s.y);
        }
        if (lineBottom)
        {
            var s = lineBottom.rectTransform.sizeDelta;
            lineBottom.rectTransform.sizeDelta = new Vector2(width, s.y);
        }
    }

    void SetTextY(float y)
    {
        if (textRect == null) return;
        var pos = textRect.anchoredPosition;
        pos.y = y;
        textRect.anchoredPosition = pos;
    }

    void SetAlpha(Graphic graphic, float a)
    {
        if (graphic == null) return;
        Color c = graphic.color;
        c.a = a;
        graphic.color = c;
    }

    float EaseOut(float t) => 1f - (1f - t) * (1f - t);
    float EaseIn(float t) => t * t;
}

