using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LanternToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Image lanternImage;
    public Sprite closedLantern;
    public Sprite openLantern;

    [Header("Bonfire")]
    public BonFire bonFire;

    [Header("Light Objects")]
    public GameObject lightBeam;
    public GameObject lanternGlow;

    [Header("Ghost Reaction")]
    public Image ghostImage;
    public Color ghostLitColor = Color.white;
    public Color ghostDarkColor = new Color(0.85f, 0.85f, 0.85f, 1f);
    public float ghostFadeInTime = 0.35f;
    public float ghostFadeOutTime = 0.55f;

    [Header("Fire FX")]
    public GameObject fireBurst;
    public GameObject firePatch;

    [Header("Fire Flicker (no movement)")]
    public Image fireImage;                 // assign Fire child Image here
    [Range(0f, 1f)] public float minBrightness = 0.92f;
    [Range(0f, 1f)] public float maxBrightness = 1.00f;
    public float flickerSpeed = 14f;
    public float flickerSmooth = 10f;

    [Header("Timing")]
    public float igniteDelay = 0.25f;

    private bool isOn = false;
    private Coroutine igniteRoutine;
    private Coroutine ghostFadeRoutine;
    private Coroutine flickerRoutine;

    void Start()
    {
        if (fireBurst) fireBurst.SetActive(false);
        if (firePatch) firePatch.SetActive(false);

        if (ghostImage) ghostImage.color = ghostDarkColor;

        if (bonFire) bonFire.ResetToUnlit();

        StopFlickerAndResetColor();

        SetState(false);
    }

    public void Toggle()
    {
        SetState(!isOn);
    }

    void SetState(bool on)
    {
        isOn = on;

        if (lanternImage)
            lanternImage.sprite = isOn ? openLantern : closedLantern;

        if (lightBeam) lightBeam.SetActive(isOn);
        if (lanternGlow) lanternGlow.SetActive(isOn);

        if (igniteRoutine != null)
        {
            StopCoroutine(igniteRoutine);
            igniteRoutine = null;
        }

        if (ghostFadeRoutine != null)
        {
            StopCoroutine(ghostFadeRoutine);
            ghostFadeRoutine = null;
        }

        if (isOn)
        {
            igniteRoutine = StartCoroutine(IgniteSequence());
        }
        else
        {
            if (fireBurst) fireBurst.SetActive(false);
            if (firePatch) firePatch.SetActive(false);

            StopFlickerAndResetColor();

            if (ghostImage)
                ghostFadeRoutine = StartCoroutine(FadeGhostTo(ghostDarkColor, ghostFadeOutTime));
        }
    }

    IEnumerator IgniteSequence()
    {
        if (fireBurst)
        {
            fireBurst.SetActive(true);

            Animator anim = fireBurst.GetComponent<Animator>();
            if (anim) anim.Play(0, 0, 0f);
        }

        if (igniteDelay > 0f)
            yield return new WaitForSeconds(igniteDelay);

        if (bonFire) bonFire.Ignite();

        if (firePatch) firePatch.SetActive(true);

        if (ghostImage)
            ghostFadeRoutine = StartCoroutine(FadeGhostTo(ghostLitColor, ghostFadeInTime));

        StartFlicker();

        float t = bonFire ? bonFire.burnTime : 0f;
        if (t > 0f)
            yield return new WaitForSeconds(t);

        StopFlickerAndResetColor();

        if (ghostImage)
            ghostFadeRoutine = StartCoroutine(FadeGhostTo(ghostDarkColor, ghostFadeOutTime));

        if (firePatch) firePatch.SetActive(false);

        igniteRoutine = null;
    }

    void StartFlicker()
    {
        if (!fireImage) return;

        if (flickerRoutine != null)
            StopCoroutine(flickerRoutine);

        flickerRoutine = StartCoroutine(FlickerRoutine());
    }

    void StopFlickerAndResetColor()
    {
        if (flickerRoutine != null)
        {
            StopCoroutine(flickerRoutine);
            flickerRoutine = null;
        }

        if (fireImage)
        {
            Color c = fireImage.color;
            c.r = 1f;
            c.g = 1f;
            c.b = 1f;
            fireImage.color = c;
        }
    }

    IEnumerator FlickerRoutine()
    {
        float current = 1f;

        while (true)
        {
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, 0f);
            float target = Mathf.Lerp(minBrightness, maxBrightness, noise);

            current = Mathf.Lerp(current, target, Time.deltaTime * flickerSmooth);

            Color c = fireImage.color;
            c.r = current;
            c.g = current;
            c.b = current;
            fireImage.color = c;

            yield return null;
        }
    }

    IEnumerator FadeGhostTo(Color target, float duration)
    {
        if (!ghostImage)
            yield break;

        if (duration <= 0f)
        {
            ghostImage.color = target;
            yield break;
        }

        Color start = ghostImage.color;
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            ghostImage.color = Color.Lerp(start, target, t / duration);
            yield return null;
        }

        ghostImage.color = target;
    }
}