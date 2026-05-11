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

    public Entity currentTarget = null;

    public Targeting targeting;

}
