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
    public Vector2 size;

    public Entity currentTarget = null;

    public Targeting targeting;


    private void OnDrawGizmos()
    {
        // draw clearance radius
        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawWireCube(transform.position, size);
    }
}
