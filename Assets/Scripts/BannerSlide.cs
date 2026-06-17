using UnityEngine;

public class BannerSlide : MonoBehaviour
{
    public Vector2 slideFrom = new Vector2(-1400f, 0f); // offset from resting position
    public float slideTime = 0.45f;
    public float startDelay = 0.2f;

    [Header("Pop on arrival")]
    public float popScale = 1.08f;   // how much it overshoots
    public float popTime = 0.15f;    // seconds for the pop to settle

    RectTransform rt;
    Vector2 restPos;
    float delay, t;
    bool sliding, popping;
    float popT;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        restPos = rt.anchoredPosition;
    }

    void OnEnable()
    {
        delay = startDelay;
        t = 0f;
        popT = 0f;
        sliding = false;
        popping = false;
        rt.anchoredPosition = restPos + slideFrom;
        rt.localScale = Vector3.one;
    }

    void Update()
    {
        if (delay > 0f) { delay -= Time.unscaledDeltaTime; return; }

        if (!sliding && !popping)
            sliding = true;

        if (sliding)
        {
            t += Time.unscaledDeltaTime / slideTime;
            float eased = EaseOut(Mathf.Clamp01(t));
            rt.anchoredPosition = Vector2.Lerp(restPos + slideFrom, restPos, eased);

            if (t >= 1f)
            {
                rt.anchoredPosition = restPos;
                sliding = false;
                popping = true;
                popT = 0f;
            }
        }
        else if (popping)
        {
            popT += Time.unscaledDeltaTime / popTime;
            float p = Mathf.Clamp01(popT);
            // overshoot then settle
            float s = p < 0.5f
                ? Mathf.Lerp(1f, popScale, p / 0.5f)
                : Mathf.Lerp(popScale, 1f, (p - 0.5f) / 0.5f);
            rt.localScale = Vector3.one * s;
            if (popT >= 1f) { rt.localScale = Vector3.one; popping = false; }
        }
    }

    float EaseOut(float x) => 1f - Mathf.Pow(1f - x, 3f);
}
