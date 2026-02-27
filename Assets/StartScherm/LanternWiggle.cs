using UnityEngine;

public class LanternWiggle : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeAngle = 6f;
    public float shakeDuration = 0.4f;
    public float minDelay = 2f;
    public float maxDelay = 5f;
    public float shakeSpeed = 25f;

    RectTransform rt;
    Quaternion baseRot;

    float timer;
    float delay;
    float shakeTimer;
    bool shaking = false;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        baseRot = rt.localRotation;
        SetNewDelay();
    }

    void Update()
    {
        if (!shaking)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                shaking = true;
                shakeTimer = 0f;
            }
        }
        else
        {
            shakeTimer += Time.deltaTime;

            float angle = Mathf.Sin(shakeTimer * shakeSpeed) *
                          shakeAngle *
                          Mathf.Exp(-shakeTimer * 5f);

            rt.localRotation = baseRot * Quaternion.Euler(0, 0, angle);

            if (shakeTimer >= shakeDuration)
            {
                shaking = false;
                timer = 0f;
                rt.localRotation = baseRot;
                SetNewDelay();
            }
        }
    }

    void SetNewDelay()
    {
        delay = Random.Range(minDelay, maxDelay);
    }
}