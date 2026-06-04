using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] Sprite[] directionalSprites;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        //Finds the SpriteRenderer so it can change the sprite.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    //Changes sprite on key input.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            spriteRenderer.sprite = directionalSprites[0];
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            spriteRenderer.sprite = directionalSprites[1];
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            spriteRenderer.sprite = directionalSprites[2];
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spriteRenderer.sprite = directionalSprites[3];
        }
    }

}
