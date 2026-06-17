using UnityEngine;

public class PopIn : MonoBehaviour
{
    public float startDelay = 0.7f;   // Victory: 0.7, Banner: 1.0
    public float popTime = 0.35f;
    public float overshoot = 1.2f;    // Victory: 1.2, Banner: 1.08
    public bool hoverAfter = true;    // true for Victory, false for Banner
    public float hoverSpeed = 2f;
    public float hoverAmount = 8f;

    RectTransform rt;
    Vector2 basePos;
    float delay, t;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        basePos = rt.anchoredPosition;
    }

    void OnEnable()
    {
        delay = startDelay;
        t = 0f;
        rt.localScale = Vector3.zero;
        rt.anchoredPosition = basePos;
    }

    void Update()
    {
        if (delay > 0f) { delay -= Time.unscaledDeltaTime; return; }

        if (t < 1f)
        {
            t += Time.unscaledDeltaTime / popTime;
            float c = Mathf.Clamp01(t);
            float s;
            if (c < 0.7f)
                s = Mathf.Sin((c / 0.7f) * Mathf.PI * 0.5f) * overshoot; // grow past 1
            else
                s = Mathf.Lerp(overshoot, 1f, (c - 0.7f) / 0.3f);        // settle back
            rt.localScale = Vector3.one * s;
        }
        else if (hoverAfter)
        {
            rt.anchoredPosition = basePos + new Vector2(0, Mathf.Sin(Time.unscaledTime * hoverSpeed) * hoverAmount);
        }
    }
}
