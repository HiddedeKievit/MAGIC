// temp comment to fix stuff hopefully

using UnityEngine;

public class Entity : MonoBehaviour, IHasHealth, IFinishable
{
    [Header("Health")]
    [SerializeField] private int health = 5;

    public int Health
    {
        get => health; set
        {
            health = value;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void ReachEnd()
    {
        // WIP: deal damage to tower

        // delete self
        Destroy(gameObject);
    }
}
