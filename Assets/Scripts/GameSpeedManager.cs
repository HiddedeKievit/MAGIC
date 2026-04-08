using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedManager : MonoBehaviour
{
    public static GameSpeedManager Instance { get; private set; }

    [Header("Speed Settings")]
    [SerializeField] private float normalSpeed = 1f;
    [SerializeField] private float fastSpeed = 2f;
    [SerializeField] private float ultraSpeed = 3f;

    [Header("Star UI")]
    [SerializeField] private Image speedImage;
    [SerializeField] private Sprite yellowStar; // x1
    [SerializeField] private Sprite greenStar;  // x2
    [SerializeField] private Sprite purpleStar; // x3

    [Header("Pop Animation")]
    [SerializeField] private float popScale = 1.15f;
    [SerializeField] private float popDuration = 0.12f;

    private float baseFixedDeltaTime;
    private Vector3 originalScale;
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
        if (speedImage != null)
            originalScale = speedImage.rectTransform.localScale;

        SetSpeed(normalSpeed);
    }

    public void ToggleSpeed()
    {
        if (Time.timeScale == 0f)
            return;

        if (Mathf.Approximately(CurrentSpeed, normalSpeed))
        {
            SetSpeed(fastSpeed);   // x2
        }
        else if (Mathf.Approximately(CurrentSpeed, fastSpeed))
        {
            SetSpeed(ultraSpeed);  // x3
        }
        else
        {
            SetSpeed(normalSpeed); // back to x1
        }
    }

    public void SetSpeed(float speed)
    {
        CurrentSpeed = speed;
        Time.timeScale = speed;
        Time.fixedDeltaTime = baseFixedDeltaTime * speed;

        UpdateSpeedVisual();
        PlayPop();
    }

    public void ResetSpeed()
    {
        SetSpeed(normalSpeed);
    }

    private void UpdateSpeedVisual()
    {
        if (speedImage == null) return;

        if (Mathf.Approximately(CurrentSpeed, normalSpeed))
            speedImage.sprite = yellowStar;
        else if (Mathf.Approximately(CurrentSpeed, fastSpeed))
            speedImage.sprite = greenStar;
        else
            speedImage.sprite = purpleStar;
    }

    private void PlayPop()
    {
        if (speedImage == null) return;

        if (popRoutine != null)
            StopCoroutine(popRoutine);

        popRoutine = StartCoroutine(PopAnimation());
    }

    private IEnumerator PopAnimation()
    {
        RectTransform rt = speedImage.rectTransform;

        Vector3 startScale = originalScale;
        Vector3 targetScale = originalScale * popScale;

        float halfDuration = popDuration * 0.5f;
        float t = 0f;

        while (t < halfDuration)
        {
            t += Time.unscaledDeltaTime;
            float lerp = t / halfDuration;
            rt.localScale = Vector3.Lerp(startScale, targetScale, lerp);
            yield return null;
        }

        t = 0f;

        while (t < halfDuration)
        {
            t += Time.unscaledDeltaTime;
            float lerp = t / halfDuration;
            rt.localScale = Vector3.Lerp(targetScale, originalScale, lerp);
            yield return null;
        }

        rt.localScale = originalScale;
        popRoutine = null;
    }
}