using UnityEngine;

public class RewardSwoop : MonoBehaviour
{
    public Vector2 startOffset = new Vector2(900f, -150f); // where it flies in from
    public float swoopTime = 0.6f;
    public float startDelay = 0.1f;
    public float hoverAngle = 2.5f;   // degrees of rocking
    public float hoverSpeed = 1.5f;
    public float bobAmount = 10f;     // pixels up/down

    RectTransform rt;
    Vector2 targetPos;
    float delay, t;
    bool arrived;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        targetPos = rt.anchoredPosition; // remembers its placed position once
    }

    void OnEnable()
    {
        delay = startDelay;
        t = 0f;
        arrived = false;
        rt.anchoredPosition = targetPos + startOffset;
        rt.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (delay > 0f) { delay -= Time.unscaledDeltaTime; return; }

        if (!arrived)
        {
            t += Time.unscaledDeltaTime / swoopTime;
            float eased = 1f - Mathf.Pow(1f - Mathf.Clamp01(t), 3f); // fast in, soft landing
            rt.anchoredPosition = Vector2.Lerp(targetPos + startOffset, targetPos, eased);
            if (t >= 1f) arrived = true;
        }
        else
        {
            float wave = Mathf.Sin(Time.unscaledTime * hoverSpeed);
            rt.localRotation = Quaternion.Euler(0, 0, wave * hoverAngle);
            rt.anchoredPosition = targetPos + new Vector2(0, Mathf.Sin(Time.unscaledTime * hoverSpeed * 0.8f) * bobAmount);
        }
    }
}
