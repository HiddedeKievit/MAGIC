using UnityEngine;
using UnityEngine.EventSystems;

public class HoverGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Glow (CanvasGroup on child)")]
    public CanvasGroup glowGroup;

    [Header("Animation")]
    public float fadeInTime = 0.10f;
    public float fadeOutTime = 0.12f;
    public float maxGlowAlpha = 0.65f;

    [Header("Scale")]
    public Transform targetToScale; // usually this plank transform
    public float hoverScale = 1.04f;
    public float pressScale = 0.98f;
    public float scaleSpeed = 18f;

    float _targetAlpha = 0f;
    float _targetScale = 1f;
    float _currentScale = 1f;

    void Reset()
    {
        targetToScale = transform;
    }

    void Awake()
    {
        if (targetToScale == null) targetToScale = transform;

        // Ensure starting state
        _currentScale = 1f;
        _targetScale = 1f;
        if (glowGroup != null) glowGroup.alpha = 0f;
    }

    void Update()
    {
        // Smooth alpha
        if (glowGroup != null)
        {
            float t = (_targetAlpha > glowGroup.alpha) ? fadeInTime : fadeOutTime;
            float speed = (t <= 0.0001f) ? 999f : (1f / t);
            glowGroup.alpha = Mathf.MoveTowards(glowGroup.alpha, _targetAlpha, speed * Time.unscaledDeltaTime);
        }

        // Smooth scale
        _currentScale = Mathf.Lerp(_currentScale, _targetScale, Time.unscaledDeltaTime * scaleSpeed);
        targetToScale.localScale = Vector3.one * _currentScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _targetAlpha = maxGlowAlpha;
        _targetScale = hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _targetAlpha = 0f;
        _targetScale = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _targetScale = pressScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Return to hover scale if still hovering
        _targetScale = hoverScale;
    }
}