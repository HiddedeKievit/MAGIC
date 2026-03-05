using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintSpeed = 5f;
    [SerializeField] private bool sprintState;
    
    [Header("SpriteChanger")]
    public SpriteChanger changer;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    //Finds rigidbody & sets sprintState to false
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprintState = false;
    }
    
    //Checks for sprint keyinput(LeftShift),
    //Increases/decreases speed depending on sprintState
    //Caps speed at minimum 5 & maximum 10.
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintState = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintState = false;
        }

        if (sprintState == true)
        {
            moveSpeed += sprintSpeed;
        }
        else 
        {
            moveSpeed -= sprintSpeed;
        }

        if (moveSpeed <= 5)
        {
            moveSpeed = 5;
        }

        if (moveSpeed >= 10)
        {
            moveSpeed = 10;
        }
    }

    //Using the input system to be able to move around + calls on SpriteChange script
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        changer.SpriteChange();
        gameObject.GetComponent<SpriteRenderer>().sprite = changer.newSprite;
    }

}
