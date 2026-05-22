using UnityEngine;


public class ProjectileData : MonoBehaviour
{
    public int Piercing = 1;
    public int Damage = 1;
    public float Speed = 0.1f;

    public float Lifetime = 5;
    public float Timer;
    public bool isDead = false;

    public bool isCirlingTarget = false;
    public bool isHomingTarget = false;

    public Transform target;

    public Vector2Int gridPosition;
}
