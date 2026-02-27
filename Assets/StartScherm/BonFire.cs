using UnityEngine;
using System.Collections; 

public class BonFire : MonoBehaviour
{
    public GameObject wood;
    public GameObject fire;

    public float burnTime = 3f; // seconds fire stays on

    private Coroutine burnCoroutine;

    public void Ignite()
    {
        if (burnCoroutine != null)
            StopCoroutine(burnCoroutine);

        wood.SetActive(false);
        fire.SetActive(true);

        burnCoroutine = StartCoroutine(BurnRoutine());
    }

    IEnumerator BurnRoutine()
    {
        yield return new WaitForSeconds(burnTime);

        fire.SetActive(false);
        wood.SetActive(true);
    }
}