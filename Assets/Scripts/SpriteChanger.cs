using Unity.VisualScripting;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField] Sprite[] directionalSprites;
    public Sprite newSprite;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = directionalSprites[0];
    }
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
