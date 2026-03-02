using Unity.VisualScripting;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] Sprite[] directionalSprites;
    public Sprite newSprite;

    //Finds the SpriteRenderer so it can change the sprite.
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = directionalSprites[0];
    }

    //Changes sprite on key input.
    public void SpriteChange()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            newSprite = directionalSprites[0];
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            newSprite = directionalSprites[1];
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            newSprite = directionalSprites[2];
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            newSprite = directionalSprites[3];
        }
    }

}
