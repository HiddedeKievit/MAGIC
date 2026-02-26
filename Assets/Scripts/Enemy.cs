using UnityEngine;

public class Enemy : MonoBehaviour, ICanMove, IHasHealth
{
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float baseRotateSpeed = 1f;
    [SerializeField] private float baseSize = 1f;
    [SerializeField] private int health = 5;

    public float BaseMovementSpeed => baseMovementSpeed;
    public float BaseRotateSpeed => baseRotateSpeed;
    public float BaseSize => baseSize;
    public int Health => health;
}
