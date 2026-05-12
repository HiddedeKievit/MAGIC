using UnityEngine;

public class Entity : MonoBehaviour, IFinishable
{

    [SerializeField] private int health = 5;
    public float TargetClearance = 1f;
    public float MovementSpeed = 1f;
    public bool CanMove = true;

    public Vector3 Target;

    public bool isDead = false;
    public Vector2Int gridPosition = new();

    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                isDead = true;
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
        isDead = true;
    }
}
