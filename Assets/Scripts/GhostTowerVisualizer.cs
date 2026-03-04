using UnityEngine;

public class GhostTowerVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private SpriteRenderer gridSprite;

    private float rangeSpriteWorldWidth;

    void Awake()
    {
        if (rangeSprite == null || gridSprite == null)
        {
            Debug.LogError("GhostTowerVisual references missing!");
            enabled = false;
            return;
        }

        // TRUE world width at scale 1
        rangeSpriteWorldWidth =
            rangeSprite.sprite.rect.width /
            rangeSprite.sprite.pixelsPerUnit;
    }

    public void Initialize(TurretData data)
    {
        SetRange(data.range);
        SetGridSize(data.towerGrid);
    }

    public void SetRange(float range)
    {
        float diameter = range * 2f;
        float scale = diameter / rangeSpriteWorldWidth;

        rangeSprite.transform.localScale =
            new Vector3(scale, scale, 1f);
    }

    public void SetGridSize(Vector2 size)
    {
        float spriteWorldWidth =
            gridSprite.sprite.rect.width /
            gridSprite.sprite.pixelsPerUnit;

        float spriteWorldHeight =
            gridSprite.sprite.rect.height /
            gridSprite.sprite.pixelsPerUnit;

        gridSprite.transform.localScale = new Vector3(
            size.x / spriteWorldWidth,
            size.y / spriteWorldHeight,
            1f
        );
    }

    public void SetGridValid(bool valid, Sprite validSprite, Sprite invalidSprite)
    {
        gridSprite.sprite = valid ? validSprite : invalidSprite;
    }
}