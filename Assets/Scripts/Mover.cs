using UnityEngine;

public class Mover : MonoBehaviour
{
    private ICanMove stats;

    void Start()
    {
        stats = GetComponent<ICanMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // dont do anything if not moving.
        if (!stats.CanMove)
            return;

        // move towards target
        MoveAndRotate(stats.CurrentSpeed, stats.CurrentRotateSpeed);
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
        Vector2 direction = ( stats.Target - transform.position ).normalized;

        // angle i want to be
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // rotate towards angle i want to be
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
    }
}
