using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private TurretData selectedTurret;
    private GameObject ghostTurret;
    private SpriteRenderer towerGridRenderer;

    [Header("Placement")]
    [SerializeField] private LayerMask placementLayer;
    [SerializeField] private Sprite validSprite;
    [SerializeField] private Sprite invalidSprite;

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
        ghostTurret = Instantiate(turret.ghostPrefab);

        foreach (var col in ghostTurret.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        towerGridRenderer = ghostTurret.GetComponentInChildren<SpriteRenderer>();

        if (towerGridRenderer == null)
        {
            Debug.LogError("Ghost prefab needs a TowerGrid SpriteRenderer child!");
            return;
        }

        towerGridRenderer.sprite = validSprite;
        ResizeTowerGrid(towerGridRenderer, selectedTurret.TowerGrid);
    }

    void Update()
    {
        if (!ghostTurret) return;

        FollowMouse();

        bool canPlace = CanPlaceAt(
            ghostTurret.transform.position,
            selectedTurret.TowerGrid
        );

        towerGridRenderer.sprite = canPlace ? validSprite : invalidSprite;

        if (Input.GetMouseButtonDown(0) && canPlace)
            Place();

        if (Input.GetMouseButtonDown(1))
            ClearPlacement();
    }

    void FollowMouse()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        ghostTurret.transform.position = pos;
    }

    void Place()
    {
        Instantiate(
            selectedTurret.turretPrefab,
            ghostTurret.transform.position,
            Quaternion.identity
        ).layer = LayerMask.NameToLayer("PlacementBlocker");

        ClearPlacement();
    }

    void ClearPlacement()
    {
        if (ghostTurret) Destroy(ghostTurret);
        ghostTurret = null;
        selectedTurret = null;
        towerGridRenderer = null;
    }

    bool CanPlaceAt(Vector3 pos, Vector2 size)
    {
        return !Physics2D.OverlapBox(pos, size, 0f, placementLayer);
    }

    void ResizeTowerGrid(SpriteRenderer sr, Vector2 targetSize)
    {
        Vector2 spriteSize = sr.sprite.bounds.size;

        sr.transform.localScale = new Vector3(
            targetSize.x / spriteSize.x,
            targetSize.y / spriteSize.y,
            1f
        );
    }
}