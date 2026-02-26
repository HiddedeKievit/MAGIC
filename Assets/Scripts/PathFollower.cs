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
        if (Path == null)
            target = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // if theres no path, im not following a path.
        if (Path == null)
            IsFollowingPath = false;

        // if im following a path, update the target.
        if (IsFollowingPath)
            target = Path.checkpoints[index].position;


        float distanceToTarget = Vector3.Distance(transform.position, target);

        // if close to the target
        if (distanceToTarget < Mover.TargetClearance)
        {
            // if i have a path, ill now be following that path

            if (Path != null)
            {
                IsFollowingPath = true;

                // last checkpoint, deal damage and destroy enemy
                if (index + 1 >= Path.checkpoints.Length)
                {
                    // kill damage
                    Destroy(gameObject);
                    return;
                }


                // next checkpoint
                if (index + 1 < Path.checkpoints.Length)
                    index++;

            }
            return;
        }


        // if you end up here, you are not close to a target so move towards target
        MoveAndRotate(Mover.BaseMovementSpeed, Mover.BaseRotateSpeed);
    }


    // Though, should pathfollower be a seperate thing from a new script: Mover? like:
    // pathfollower handles the current path location and stuff, mover handles moving towards a target in general
    public void SetTarget(Vector3 position)
    {
        IsFollowingPath = false;
        target = position.normalized;
    }

    void MoveAndRotate(float moveSpeed, float rotateSpeed)
    {
        MoveToTarget(moveSpeed);
        RotateToTarget(rotateSpeed);
    }

    void MoveToTarget(float speed)
    {
        // move forward
        transform.position = transform.position + Time.deltaTime * speed * transform.up;
    }

    void RotateToTarget(float rotateSpeed)
    {
        // direction from me to target
        Vector2 direction = ( target - transform.position ).normalized;

        // angle i want to be
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // rotate towards angle i want to be
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
