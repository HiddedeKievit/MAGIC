using UnityEngine;

public class FireBurstEnd : MonoBehaviour
{
    public GameObject firePatch;
    public float burstSeconds = 0.45f; // match your burst length

    void OnEnable()
    {
        CancelInvoke();
        Invoke(nameof(Finish), burstSeconds);
    }

    void Finish()
    {
        if (firePatch) firePatch.SetActive(true);
        gameObject.SetActive(false);
    }
}