using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private bool sprintState = false;

    [Header("SpriteChanger")]
    public SpriteChanger changer;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Finds rigidbody & sets sprintState to false
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // get input
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        moveInput = new Vector2(horizontal, vertical);

        // get sprint key
        sprintState = Input.GetKey(KeyCode.LeftShift);
        // if sprinting = true, set movement to 10 otherwise to 5
        moveSpeed = sprintState ? sprintSpeed : moveSpeed;

        // set velocity based on what key is pressed
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
