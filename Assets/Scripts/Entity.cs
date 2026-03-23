using UnityEngine;

public class Entity : MonoBehaviour, IFinishable
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
        GameObject towerhmObj = GameObject.FindGameObjectWithTag("TowerHealthManager");

        if (towerhmObj && towerhmObj.TryGetComponent(out HealthManager healthManager))
        {
            healthManager.Health -= 1;
        }


        // delete self
        Destroy(gameObject);

        enabled = false;
    }
}
