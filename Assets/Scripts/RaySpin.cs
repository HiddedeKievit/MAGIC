using UnityEngine;
using UnityEngine.UI;

public class RaySpin : MonoBehaviour
{
    [Header("Animation")]
    public float duration = 1.2f;
    public float maxScaleX = 6f;
    public float minScaleX = 0f;
    public float startDelay = 0f;

    [Header("Fade")]
    public float maxAlpha = 0.65f;

    private RectTransform rt;
    private Image img;
    private Color baseColor;
    private float timer;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        baseColor = img.color;

        rt.anchoredPosition = Vector2.zero;
    }

    private void OnEnable()
    {
        timer = -startDelay;
        rt.anchoredPosition = Vector2.zero;
    }

    private void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer < 0f)
        {
            SetRay(0f, 0f);
            return;
        }

        float progress = (timer % duration) / duration;

        float scaleX = Mathf.Lerp(minScaleX, maxScaleX, progress);

        float alpha;
        if (progress < 0.2f)
        {
            alpha = Mathf.Lerp(0f, maxAlpha, progress / 0.2f);
        }
        else
        {
            alpha = Mathf.Lerp(maxAlpha, 0f, (progress - 0.2f) / 0.8f);
        }

        SetRay(scaleX, alpha);
    }

    private void SetRay(float scaleX, float alpha)
    {
        rt.localScale = new Vector3(scaleX, 1f, 1f);
        img.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }
}
