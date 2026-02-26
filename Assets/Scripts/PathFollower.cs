// temp comment to fix stuff hopefully

using UnityEngine;


public class PathFollower : MonoBehaviour
{
    private ICanMove Mover;

    public PathManager Path;

    public bool IsFollowingPath = true;

    private Vector3 target;
    private int index = 0;

    void Start()
    {
        Mover = GetComponent<ICanMove>();
        target = Path.checkpoints[index].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFollowingPath)
            target = Path.checkpoints[index].position;

        float distanceToTarget = Vector3.Distance(transform.position, target);

        // if close to the target
        if (distanceToTarget < Mover.BaseSize)
        {

            // slow by dividing the distance by the base size
            float slowFactor = distanceToTarget / Mover.BaseSize;
            float adjustedSpeed = Mover.BaseMovementSpeed * slowFactor;


            // last checkpoint, stop moving when close enough.
            if (index + 1 >= Path.checkpoints.Length && distanceToTarget <= 0.1f)
            {
                return;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * adjustedSpeed);

            // move slowly towards the last checkpoint
            MoveToTarget(adjustedSpeed, Mover.BaseRotateSpeed);


            if (index + 1 < Path.checkpoints.Length)
            {
                index++;
                return;
            }
        } else
        {
            MoveToTarget(Mover.BaseMovementSpeed, Mover.BaseRotateSpeed);
        }
    }


    void MoveToTarget(float speed, float rotateSpeed)
    {

        // direction from me to target
        Vector2 direction = ( target - transform.position ).normalized;

        // angle i want to be
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // rotate towards angle i want to be

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);


        transform.position = transform.position + Time.deltaTime * speed * transform.up;
    }
}
