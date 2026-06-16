using UnityEngine;
using UnityEngine.UI;

public class RayOriginSetup : MonoBehaviour
{
    [Header("Ray Shape")]
    [SerializeField] private float rayWidth = 1200f;
    [SerializeField] private float rayHeight = 45f;

    [Header("Ray Animation")]
    [SerializeField] private float duration = 1.2f;
    [SerializeField] private float maxScaleX = 6f;
    [SerializeField] private float minScaleX = 0f;
    [SerializeField] private float maxAlpha = 0.55f;

    [Header("Timing")]
    [SerializeField] private float maxRandomDelay = 0.15f;

    [Header("Random Variation")]
    [SerializeField] private float minScaleMultiplier = 0.75f;
    [SerializeField] private float maxScaleMultiplier = 1.25f;
    [SerializeField] private float minAlphaMultiplier = 0.4f;
    [SerializeField] private float maxAlphaMultiplier = 1f;

    [Header("Auto Setup")]
    [SerializeField] private bool setupOnStart = true;

    private void Start()
    {
        if (setupOnStart)
        {
            SetupRays();
        }
    }

    [ContextMenu("Setup Rays")]
    private void SetupRays()
    {
        int rayCount = transform.childCount;

        for (int i = 0; i < rayCount; i++)
        {
            RectTransform ray = transform.GetChild(i).GetComponent<RectTransform>();
            if (ray == null) continue;

            Image image = ray.GetComponent<Image>();
            RaySpin raySpin = ray.GetComponent<RaySpin>();

            // Put every ray exactly on the origin
            ray.anchoredPosition = Vector2.zero;

            // Important: grow from left-middle point
            ray.pivot = new Vector2(0f, 0.5f);

            // Shape of the ray
            ray.sizeDelta = new Vector2(rayWidth, rayHeight);
            ray.localScale = Vector3.one;

            // Random burst angle, not a perfect circle
            float angle = GetRandomBurstAngle();
            ray.localRotation = Quaternion.Euler(0f, 0f, angle);

            if (image != null)
            {
                image.type = Image.Type.Simple;
            }

            if (raySpin != null)
            {
                raySpin.duration = Random.Range(duration * 0.85f, duration * 1.25f);
                raySpin.maxScaleX = maxScaleX * Random.Range(minScaleMultiplier, maxScaleMultiplier);
                raySpin.minScaleX = minScaleX;
                raySpin.maxAlpha = maxAlpha * Random.Range(minAlphaMultiplier, maxAlphaMultiplier);

                // Random delay, not ordered delay.
                // This prevents the spinning circle effect.
                raySpin.startDelay = Random.Range(0f, maxRandomDelay);
            }
        }
    }

    private float GetRandomBurstAngle()
    {
        float roll = Random.value;

        // Angle guide:
        // 0   = right
        // 90  = up
        // 180 = left
        // 270 = down

        if (roll < 0.65f)
        {
            // Most rays go upward behind the card
            return Random.Range(25f, 155f);
        }
        else if (roll < 0.78f)
        {
            // Some rays go to the right
            return Random.Range(-20f, 25f);
        }
        else if (roll < 0.90f)
        {
            // Some rays go to the left
            return Random.Range(155f, 205f);
        }
        else
        {
            // A few rays go downward
            return Random.Range(230f, 310f);
        }
    }
}