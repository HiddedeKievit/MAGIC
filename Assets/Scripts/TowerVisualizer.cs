using UnityEngine;

[RequireComponent(typeof(TowerTargeting))]
[RequireComponent(typeof(TowerSelection))]
public class TowerRangeVisualizer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rangeSprite;

    private TowerTargeting targeting;
    private TowerSelection selection;

    private float lastRange = -1f;
    private bool lastSelected = false;
    private float spriteOriginalWidth; // cached original sprite width in world units

    void Awake()
    {
        targeting = GetComponent<TowerTargeting>();
        selection = GetComponent<TowerSelection>();

        if (rangeSprite == null)
        {
            Debug.LogError("Range sprite not assigned on " + name);
            enabled = false;
            return;
        }

        // Center the sprite
        rangeSprite.transform.localPosition = Vector3.zero;
        rangeSprite.enabled = false;

        // Cache the original sprite width
        spriteOriginalWidth = rangeSprite.bounds.size.x;
        if (spriteOriginalWidth <= 0f)
        {
            Debug.LogError("Range sprite width is zero! Check the sprite import settings.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        // Toggle visibility only when selection changes
        if (selection.IsSelected != lastSelected)
        {
            rangeSprite.enabled = selection.IsSelected;
            lastSelected = selection.IsSelected;
        }

        // Update scale only if range changed
        if (Mathf.Abs(targeting.Range - lastRange) > 0.01f)
        {
            float diameter = targeting.Range * 2f;
            float scale = diameter / spriteOriginalWidth;
            rangeSprite.transform.localScale = new Vector3(scale, scale, 1f);
            lastRange = targeting.Range;
        }
    }

    void LateUpdate()
    {
        // Freeze rotation **in world space** so it never rotates with parent
        rangeSprite.transform.rotation = Quaternion.identity; 
    }
}