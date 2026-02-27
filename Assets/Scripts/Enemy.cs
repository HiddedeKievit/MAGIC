using UnityEngine;

public class Enemy : MonoBehaviour, ICanMove, IHasHealth
{
    [Header("Stats")]
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float baseRotateSpeed = 1f;
    [SerializeField] private float baseSize = 1f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 5;
    private int health;

    public float BaseMovementSpeed => baseMovementSpeed;
    public float BaseRotateSpeed => baseRotateSpeed;
    public float BaseSize => baseSize;
    public int Health => health;

    void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Hier kan je enemy specifieke rewards neerzetten op death
        Destroy(gameObject);
    }
}