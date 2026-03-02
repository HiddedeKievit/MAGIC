// temp comment to fix stuff hopefully

using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float targetClearance = 1f;
    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private bool canMove = true;

    private float angle;

    // just to show for debug


    public Vector3 Target;
    public float TargetClearence => targetClearance;
    public float DistanceToTarget;



    // Update is called once per frame
    void Update()
    {
        DistanceToTarget = Vector2.Distance(transform.position, Target);


        // dont do anything if not moving.
        if (!canMove)
            return;

        // move forwards by speed
        transform.position = transform.position + Time.deltaTime * movementSpeed * transform.up;


        // direction from me to target
        Vector2 direction = ( Target - transform.position ).normalized;

        // angle i want to be
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // rotate towards angle i want to be



        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }



    // visualize the clearance and speed

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        // draw line for speed
        Vector3 dir = ( transform.up * movementSpeed ) / 10;
        Gizmos.DrawRay(transform.position, dir);
        Gizmos.DrawSphere(transform.position + dir, 0.025f);

        // draw clearance radius
        Gizmos.color = new Color(1f, 1f, 1f, 0.1f);
        Gizmos.DrawWireSphere(transform.position, targetClearance);



        Gizmos.color = new Color(0f, 1f, 0f, 0.1f);
        Gizmos.DrawWireSphere(Target, targetClearance);

    }
}
