using UnityEngine;

public class WobbleWalk : MonoBehaviour
{
    // how fast it rotates
    [SerializeField] private float wobbleSpeed = 6f;
    // how high it can go
    [SerializeField] private float bobHeight = 0.1f;
    // how much it can rotate
    [SerializeField] private float tiltAmount = 10f;
    // how fast it returns to normal without input
    [SerializeField] private float returnSpeed = 8f;

    // start pos and rot
    private Vector3 baseLocalPos;
    private float currentTilt;

    private void Awake()
    {
        baseLocalPos = transform.localPosition;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isMoving = horizontal != 0 || vertical != 0;

        Vector3 pos = baseLocalPos;

        if (isMoving)
        {
            float wave = Mathf.Sin(Time.time * wobbleSpeed);

            pos.y += Mathf.Abs(wave) * bobHeight;
            currentTilt = wave * tiltAmount;
        } else
        {
            currentTilt = Mathf.Lerp(currentTilt, 0f, Time.deltaTime * returnSpeed);
        }

        transform.localPosition = pos;
        transform.localRotation = Quaternion.Euler(0f, 0f, currentTilt);
    }
}