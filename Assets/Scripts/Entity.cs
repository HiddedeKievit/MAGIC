using UnityEngine;

public class Entity : MonoBehaviour, ICanMove, IHasHealth, IPathable, IFinishable
{
    [SerializeField] private float baseMovementSpeed = 1f;
    [SerializeField] private float baseRotateSpeed = 1f;
    [SerializeField] private int health = 5;
    [SerializeField] private bool canMove = true;
    [SerializeField] private float targetClearance = 1f;
    [SerializeField] private PathManager path;

    // no need to have these available in the editor
    private float currentSpeed;
    private float currentRotateSpeed;
    private Vector3 target;


    public float BaseMovementSpeed => baseMovementSpeed;
    public float BaseRotateSpeed => baseRotateSpeed;
    public float TargetClearance => targetClearance;

    public float CurrentSpeed
    {
        get => currentSpeed; set { currentSpeed = value; }
    }
    public float CurrentRotateSpeed
    {
        get => currentRotateSpeed; set { currentRotateSpeed = value; }
    }
    public Vector3 Target
    {
        get => target; set { target = value; }
    }
    public bool CanMove
    {
        get => canMove; set { canMove = value; }
    }

    public int Health
    {
        get => health; set
        {
            health = value;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public PathManager Path { get => path; set { path = value; } }

    // init doesnt run?
    void Awake()
    {
        // set current speed and rotation to base speed/rot. & current target to itself.

        CurrentSpeed = baseMovementSpeed;
        CurrentRotateSpeed = baseRotateSpeed;
        Target = transform.localPosition;
    }


    public void ReachEnd()
    {
        // WIP: deal damage to tower

        // delete self
        Destroy(gameObject);
    }




    // visualize the clearance and speed

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
