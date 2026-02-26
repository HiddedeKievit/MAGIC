using UnityEngine;

public class Enemy : MonoBehaviour, ICanMove, IHasHealth
{
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float baseRotateSpeed = 1f;
    [SerializeField] private float targetClearance = 1f;
    [SerializeField] private int health = 5;

    public float BaseMovementSpeed => baseMovementSpeed;
    public float BaseRotateSpeed => baseRotateSpeed;
    public float TargetClearance => targetClearance;
    public int Health => health;



    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // draw line for speed
        Vector3 dir = ( transform.up * baseMovementSpeed ) / 10;
        Gizmos.DrawRay(transform.position, dir);
        Gizmos.DrawSphere(transform.position + dir, 0.025f);

        Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
        // draw clearance radius
        Gizmos.DrawWireSphere(transform.position, TargetClearance);

    }
}
