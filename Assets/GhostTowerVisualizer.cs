using UnityEngine;

public class GhostTowerVisualizer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer rangeSprite;
    [SerializeField] private SpriteRenderer gridSprite;

    private float rangeSpriteOriginalWidth;

    void Awake()
    {
        if (rangeSprite == null || gridSprite == null)
        {
            Debug.LogError("GhostTowerVisual references missing!");
            enabled = false;
            return;
        }

        rangeSpriteOriginalWidth = rangeSprite.bounds.size.x;
    }

    public void Initialize(TurretData data)
    {
        SetRange(data.range);
        SetGridSize(data.towerGrid);
    }

    public void SetRange(float range)
    {
        float diameter = range * 2f;
        float scale = diameter / rangeSpriteOriginalWidth;
        rangeSprite.transform.localScale = new Vector3(scale, scale, 1f);
    }

    public void SetGridSize(Vector2 size)
    {
        Vector2 spriteSize = gridSprite.sprite.bounds.size;

        gridSprite.transform.localScale = new Vector3(
            size.x / spriteSize.x,
            size.y / spriteSize.y,
            1f
        );
    }

    public void SetGridValid(bool valid, Sprite validSprite, Sprite invalidSprite)
    {
        gridSprite.sprite = valid ? validSprite : invalidSprite;
    }
}