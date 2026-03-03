using System.Collections;
using UnityEngine;

public class BonFire : MonoBehaviour
{
    public GameObject wood;
    public GameObject fire;
    public float burnTime = 3f;

    private Coroutine burnCoroutine;

    void Awake()
    {
        ResetToUnlit();
    }

    public void ResetToUnlit()
    {
        if (wood) wood.SetActive(true);
        if (fire) fire.SetActive(false);

        if (burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
            burnCoroutine = null;
        }
    }

    public void Ignite()
    {
        if (!fire) return;

        fire.SetActive(true);

        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        burnCoroutine = StartCoroutine(BurnRoutine());
    }

    private IEnumerator BurnRoutine()
    {
        yield return new WaitForSeconds(burnTime);

        if (fire) fire.SetActive(false);

        burnCoroutine = null;
    }
}