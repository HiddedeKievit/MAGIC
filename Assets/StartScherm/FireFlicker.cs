using UnityEngine;
using UnityEngine.UI;

public class FireFlicker : MonoBehaviour
{
    public Image targetImage;

    [Range(0f, 1f)] public float minAlpha = 0.96f;
    [Range(0f, 1f)] public float maxAlpha = 1.00f;

    public float flickerSpeed = 10f;
    public float smoothing = 10f;

    float currentA;

    void Awake()
    {
        if (!targetImage) targetImage = GetComponent<Image>();
        currentA = targetImage ? targetImage.color.a : 1f;
    }

    void OnEnable()
    {
        if (!targetImage) targetImage = GetComponent<Image>();
        if (targetImage) currentA = targetImage.color.a;
    }

    void Update()
    {
        if (!targetImage) return;

        float n = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
        float targetA = Mathf.Lerp(minAlpha, maxAlpha, n);

        currentA = Mathf.Lerp(currentA, targetA, Time.deltaTime * smoothing);

        Color c = targetImage.color;
        c.a = currentA;
        targetImage.color = c;
    }
}