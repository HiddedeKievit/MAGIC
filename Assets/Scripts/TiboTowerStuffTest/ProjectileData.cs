using UnityEngine;


public class ProjectileData : MonoBehaviour
{
    public int Piercing = 1;
    public int Damage = 1;
    public float Speed = 0.1f;

    public Vector2 hitbox;

    public float Lifetime = 5;
    public float Timer;
    public bool isDead = false;

    public bool isCirlingTarget = false;
    public bool isHomingTarget = false;

    public Transform target;

    public Vector2Int gridPosition;

    private void OnDrawGizmos()
    {
        // draw clearance radius
        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawWireCube(transform.position, hitbox);
    }
}
