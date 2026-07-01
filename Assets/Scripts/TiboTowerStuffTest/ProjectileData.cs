using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    // how many times can i pass trough something
    public int Piercing = 1;
    // how much damage do i deal
    public int Damage = 1;
    // how fast am i
    public float Speed = 1f;

    // when do i hit something
    public Vector2 hitbox;

    // how long can i exist
    public float Lifetime = 5;
    // how long have i existed
    public float Timer;
    // am i dead
    public bool isDead = false;

    // am i circeling my target 
    public bool isCirlingTarget = false;
    // am i rotating towards my target 
    public bool isHomingTarget = false;

    public TowerData parentTower;

    // what is my target
    public Transform target;

    // where am i in the grid
    public Vector2Int gridPosition;

    private void OnDrawGizmos()
    {
        // draw my hitbox in the scene view
        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawWireCube(transform.position, hitbox);
    }
}
