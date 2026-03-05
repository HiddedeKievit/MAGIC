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

        rangeSpriteWorldWidth =
            rangeSprite.sprite.rect.width /
            rangeSprite.sprite.pixelsPerUnit;

        gridSprite.enabled = false; // hidden by default
    }

    public void Initialize(TurretData data)
    {
        SetRange(data.range);
        SetGridSize(data.range);
    }

    public void SetRange(float range)
    {
        float diameter = range * 2f;
        float scale = diameter / rangeSpriteWorldWidth;

        Vector3 scaleVec = new Vector3(scale, scale, 1f);

        rangeSprite.transform.localScale = scaleVec;
        gridSprite.transform.localScale = scaleVec;
    }

    void SetGridSize(float range)
    {
        float diameter = range * 2f;
        float scale = diameter / rangeSpriteWorldWidth;

        gridSprite.transform.localScale =
            new Vector3(scale, scale, 1f);
    }

    public void SetPlacementValid(bool valid)
    {
        gridSprite.enabled = !valid;
    }
}