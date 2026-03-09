using UnityEngine;

public class EffectLifetime : MonoBehaviour
{
    public float lifetime;
    void Start() => Destroy(gameObject, lifetime);
}