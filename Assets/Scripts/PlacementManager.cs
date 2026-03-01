using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private TurretData selectedTurret;
    private GameObject ghostTurret;
    private SpriteRenderer footprintRenderer;

    [Header("Placement Settings")]
    [SerializeField] private LayerMask placementLayer; 
    [SerializeField] private Sprite validPlacementSprite;   // green circle
    [SerializeField] private Sprite invalidPlacementSprite; // red circle

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void StartPlacement(TurretData turret)
    {
        ClearPlacement();

        selectedTurret = turret;

        // Instantiate ghost prefab
        ghostTurret = Instantiate(turret.ghostPrefab);

        // Disable all colliders on ghost
        foreach (var col in ghostTurret.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        // Add / get footprint sprite
        footprintRenderer = ghostTurret.GetComponentInChildren<SpriteRenderer>();
        if (footprintRenderer == null)
        {
            Debug.LogError("Ghost prefab needs a child SpriteRenderer for footprint!");
        }
        else
        {
            footprintRenderer.sprite = validPlacementSprite;
            footprintRenderer.transform.localScale = new Vector3(
                selectedTurret.FootprintSize.x,
                selectedTurret.FootprintSize.y,
                1f
            );
        }
    }

    void Update()
    {
        if (ghostTurret == null || selectedTurret == null) return;

        FollowMouse();

        // Check placement
        bool canPlace = CanPlaceAt(ghostTurret.transform.position, selectedTurret.FootprintSize);

        // Update footprint sprite and color
        if (footprintRenderer != null)
        {
            footprintRenderer.sprite = canPlace ? validPlacementSprite : invalidPlacementSprite;
        }

        // Place tower if valid
        if (Input.GetMouseButtonDown(0) && canPlace)
            TryPlace();

        // Cancel placement
        if (Input.GetMouseButtonDown(1))
            CancelPlacement();
    }

    void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        ghostTurret.transform.position = mousePos;
    }

    void TryPlace()
    {
        if (!CanPlaceAt(ghostTurret.transform.position, selectedTurret.FootprintSize))
        {
            Debug.Log("Cannot place here!");
            return;
        }

        GameObject placed = Instantiate(
            selectedTurret.turretPrefab,
            ghostTurret.transform.position,
            Quaternion.identity
        );

        placed.layer = LayerMask.NameToLayer("PlacementBlocker");

        ClearPlacement();
    }

    void CancelPlacement()
    {
        ClearPlacement();
    }

    void ClearPlacement()
    {
        if (ghostTurret != null)
            Destroy(ghostTurret);

        ghostTurret = null;
        selectedTurret = null;
        footprintRenderer = null;
    }

    private bool CanPlaceAt(Vector3 position, Vector2 size)
    {
        Collider2D hit = Physics2D.OverlapBox(position, size, 0f, placementLayer);
        return hit == null;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (ghostTurret == null || selectedTurret == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(ghostTurret.transform.position, selectedTurret.FootprintSize);
    }
#endif
}