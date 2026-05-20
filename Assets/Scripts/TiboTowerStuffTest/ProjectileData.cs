using UnityEngine;


public class ProjectileData : MonoBehaviour
{
    public int Piercing = 1;
    public int Damage = 1;
    public float Speed = 0.1f;
    public Vector3 direction = Vector3.zero;

    public int Lifetime = 5;
    public float Timer;
    public bool isDead = false;

    public Vector2Int gridPosition;
}
