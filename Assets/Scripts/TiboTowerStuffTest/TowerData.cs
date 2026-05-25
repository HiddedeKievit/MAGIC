using System.Collections.Generic;
using UnityEngine;


public enum Targeting
{
    Close, Far, Strong, Weak
}

public class TowerData : MonoBehaviour
{
    public float range;
    public ProjectileData projectile;
    public float firerate;
    public float cooldown;
    public Vector2 hitbox;

    public float cost = 5f;

    public Transform currentTarget = null;

    public bool projectileCircleMe;
    public int maxProjectiles = 10;
    public List<ProjectileData> activeProjectiles;

    public Targeting targeting;

    private void Awake()
    {
        activeProjectiles = new();
    }

    private void OnDrawGizmos()
    {
        // draw clearance radius
        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireCube(transform.position, hitbox);
    }
}
