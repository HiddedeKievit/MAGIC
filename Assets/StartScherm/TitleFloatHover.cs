using UnityEngine;
using UnityEngine.EventSystems;


public class TitleFloatHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  
  
    [Header("Drift (pixels)")]
    public float baseDriftX = 3f;
    public float baseDriftY = 5f;
    public float hoverDriftX = 5f;
    public float hoverDriftY = 8f;

    [Header("Speed")]
    public float baseSpeed = 0.18f;
    public float hoverSpeed = 0.26f;

    [Header("Sway (degrees)")]
    public float baseRot = 1.2f;
    public float hoverRot = 2.0f;

    [Header("Scale")]
    public float baseScale = 1f;
    public float hoverScale = 1.04f;

    [Header("Per-letter shimmer")]
    public CanvasGroup[] letters;      // drag N,Y,X CanvasGroups here
    public float baseAlpha = 0.92f;
    public float shimmerAmount = 0.06f;
    public float shimmerSpeed = 0.6f;
    public float hoverAlphaBoost = 0.04f;

    [Header("Smoothness")]
    public float smooth = 8f;

    RectTransform rect;
    Vector2 startPos;
    Quaternion startRot;

    bool hovering;
    float seed;

    float curDriftX, curDriftY, curSpeed, curRot, curScale;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        startPos = rect.anchoredPosition;
        startRot = rect.localRotation;

        seed = Random.Range(0f, 1000f);

        curDriftX = baseDriftX;
        curDriftY = baseDriftY;
        curSpeed = baseSpeed;
        curRot = baseRot;
        curScale = baseScale;
    }

    void Update()
    {
        float targetDriftX = hovering ? hoverDriftX : baseDriftX;
        float targetDriftY = hovering ? hoverDriftY : baseDriftY;
        float targetSpeed = hovering ? hoverSpeed : baseSpeed;
        float targetRot = hovering ? hoverRot : baseRot;
        float targetScale = hovering ? hoverScale : baseScale;

        curDriftX = Mathf.Lerp(curDriftX, targetDriftX, Time.unscaledDeltaTime * smooth);
        curDriftY = Mathf.Lerp(curDriftY, targetDriftY, Time.unscaledDeltaTime * smooth);
        curSpeed = Mathf.Lerp(curSpeed, targetSpeed, Time.unscaledDeltaTime * smooth);
        curRot = Mathf.Lerp(curRot, targetRot, Time.unscaledDeltaTime * smooth);
        curScale = Mathf.Lerp(curScale, targetScale, Time.unscaledDeltaTime * smooth);

        float t = Time.unscaledTime * curSpeed;

        float nx = Mathf.PerlinNoise(seed + 10f, t) * 2f - 1f;
        float ny = Mathf.PerlinNoise(seed + 20f, t + 5f) * 2f - 1f;
        float micro = Mathf.Sin((seed + Time.unscaledTime) * 0.6f) * 0.35f;

        rect.anchoredPosition = startPos + new Vector2(nx * curDriftX, (ny + micro) * curDriftY);

        float nr = Mathf.PerlinNoise(seed + 30f, t + 10f) * 2f - 1f;
        rect.localRotation = startRot * Quaternion.Euler(0f, 0f, nr * curRot);

        rect.localScale = Vector3.one * curScale;

        // Individual shimmer per letter (different phase)
        if (letters != null)
        {
            float hoverBoost = hovering ? hoverAlphaBoost : 0f;

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == null) continue;

                float phase = seed + i * 13.37f;
                float shimmer = Mathf.Sin((Time.unscaledTime + phase) * shimmerSpeed) * shimmerAmount;

                float targetAlpha = Mathf.Clamp01(baseAlpha + hoverBoost + shimmer);
                letters[i].alpha = Mathf.Lerp(letters[i].alpha, targetAlpha, Time.unscaledDeltaTime * smooth);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) => hovering = true;
    public void OnPointerExit(PointerEventData eventData) => hovering = false;
}