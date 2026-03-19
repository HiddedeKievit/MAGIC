using System.Collections;
using UnityEngine;

public class HoverBounceSimple : MonoBehaviour
{
    public float bounceScale = 1.25f;
    public float bounceTime = 0.12f;

    private Vector3 originalScale;
    private Coroutine bounceRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void ShowAndBounce()
    {
        gameObject.SetActive(true);

        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);

        transform.localScale = originalScale;
        bounceRoutine = StartCoroutine(BounceRoutine());
    }

    public void Hide()
    {
        if (bounceRoutine != null)
            StopCoroutine(bounceRoutine);

        transform.localScale = originalScale;
        gameObject.SetActive(false);
    }

    private IEnumerator BounceRoutine()
    {
        Vector3 bigScale = originalScale * bounceScale;

        float t = 0f;
        while (t < bounceTime)
        {
            t += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(originalScale, bigScale, t / bounceTime);
            yield return null;
        }

        t = 0f;
        while (t < bounceTime)
        {
            t += Time.unscaledDeltaTime;
            transform.localScale = Vector3.Lerp(bigScale, originalScale, t / bounceTime);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}