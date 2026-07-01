using System.Collections.Generic;
using UnityEngine;


public enum Targeting
{
    Close, Far, Strong, Weak
}

public class TowerData : MonoBehaviour
{
    public Transform turret;
    // how far can i see enemies
    public float range;
    // what do i shoot
    public ProjectileData projectile;
    // how often can i shoot
    public float firerate;
    // timer for last time i've shot
    public float cooldown;
    // how big am i
    public Vector2 hitbox;

    public int damageDealt;
    public int kills;

    // how much do i cost
    public float cost = 5f;

    // my current target
    public Transform currentTarget = null;

    // should the the projectile circle me?
    public bool projectileCircleMe;
    // how many existing projectiles can i have?
    public int maxProjectiles = 10;
    // what are my active projectiles?
    public List<ProjectileData> activeProjectiles;

    // who do i target first?
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

    private void OnMouseOver()
    {
        Debug.Log($"he hovering me!! {this.name}");
        if (Input.GetMouseButtonDown(0))
        {
            // should be clicking on this tower
            if (TowerStatsNSettings.Instance.IsTower(this))
            {
                TowerStatsNSettings.Instance.SetTowerToView(null);
            } else
            {
                TowerStatsNSettings.Instance.SetTowerToView(this);
            }
        }
    }
}
