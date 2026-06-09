using System.Collections;
using UnityEngine;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance { get; private set; }

    [Header("Speed Settings")]
    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float fastSpeed = 2f;
    [SerializeField] private float ultraSpeed = 3f;

    [Header("Speed Layouts")]
    [SerializeField] private GameObject speed1;
    [SerializeField] private GameObject speed2;
    [SerializeField] private GameObject speed3;

    [Header("Pop Effect")]
    [SerializeField] private RectTransform popTarget;
    [SerializeField] private float popScale = 1.12f;
    [SerializeField] private float popDuration = 0.16f;
    [SerializeField] private float wiggleAngle = 8f;

    [Header("Optional Effects")]
    [SerializeField] private ParticleSystem sparkleBurst;
    [SerializeField] private AudioSource sfxSource;

    [Header("Speed Audio")]
    [SerializeField] private AudioClip speed2xSfx;
    [SerializeField] private AudioClip speed3xSfx;
    [SerializeField] private AudioClip normalSpeedSfx;

    private float baseFixedDeltaTime;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Coroutine popRoutine;

    public float CurrentSpeed { get; private set; } = 1f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        baseFixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        if (popTarget == null)
            popTarget = GetComponent<RectTransform>();

        if (popTarget != null)
        {
            originalScale = popTarget.localScale;
            originalRotation = popTarget.localRotation;
        }

        SetSpeed(normalSpeed, false);
    }

    public void ToggleSpeed()
    {
        if (Time.timeScale == 0f)
            return;

        if (Mathf.Approximately(CurrentSpeed, normalSpeed))
            SetSpeed(fastSpeed);
        else if (Mathf.Approximately(CurrentSpeed, fastSpeed))
            SetSpeed(ultraSpeed);
        else
            SetSpeed(normalSpeed);
    }

    public void SetSpeed(float speed)
    {
        SetSpeed(speed, true);
    }

    private void SetSpeed(float speed, bool playEffects)
    {
        CurrentSpeed = speed;
        Time.timeScale = speed;
        Time.fixedDeltaTime = baseFixedDeltaTime * speed;

        UpdateLayout();

        if (playEffects)
            PlayEffects(speed);
    }

    public void ResetSpeed()
    {
        SetSpeed(normalSpeed);
    }

    private void UpdateLayout()
    {
        if (speed1 != null) speed1.SetActive(false);
        if (speed2 != null) speed2.SetActive(false);
        if (speed3 != null) speed3.SetActive(false);

        if (Mathf.Approximately(CurrentSpeed, normalSpeed))
        {
            if (speed1 != null) speed1.SetActive(true);
        }
        else if (Mathf.Approximately(CurrentSpeed, fastSpeed))
        {
            if (speed2 != null) speed2.SetActive(true);
        }
        else
        {
            if (speed3 != null) speed3.SetActive(true);
        }
    }

    private void PlayEffects(float speed)
    {
        if (popRoutine != null)
            StopCoroutine(popRoutine);

        popRoutine = StartCoroutine(PopEffect());

        if (sparkleBurst != null)
            sparkleBurst.Play();

        PlaySpeedSound(speed);
    }

    private void PlaySpeedSound(float speed)
    {
        if (sfxSource == null)
            return;

        AudioClip clipToPlay = null;

        if (Mathf.Approximately(speed, fastSpeed))
            clipToPlay = speed2xSfx;
        else if (Mathf.Approximately(speed, ultraSpeed))
            clipToPlay = speed3xSfx;
        else if (Mathf.Approximately(speed, normalSpeed))
            clipToPlay = normalSpeedSfx;

        if (clipToPlay != null)
            sfxSource.PlayOneShot(clipToPlay);
    }

    private IEnumerator PopEffect()
    {
        if (popTarget == null)
            yield break;

        float t = 0f;

        while (t < popDuration)
        {
            t += Time.unscaledDeltaTime;
            float progress = Mathf.Clamp01(t / popDuration);

            float pop = Mathf.Sin(progress * Mathf.PI);
            float scaleMultiplier = Mathf.Lerp(1f, popScale, pop);
            float wiggle = Mathf.Sin(progress * Mathf.PI * 2f) * wiggleAngle * (1f - progress);

            popTarget.localScale = originalScale * scaleMultiplier;
            popTarget.localRotation = originalRotation * Quaternion.Euler(0f, 0f, wiggle);

            yield return null;
        }

        popTarget.localScale = originalScale;
        popTarget.localRotation = originalRotation;
        popRoutine = null;
    }
}