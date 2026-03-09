using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager Instance { get; private set; }

    private TurretData selectedTurret;
    private GameObject ghostTurret;
    private GhostTowerVisualizer ghostVisualizer;

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

        // Disable all colliders on ghost
        foreach (var col in ghostTurret.GetComponentsInChildren<Collider2D>())
            col.enabled = false;

        ghostVisualizer = ghostTurret.GetComponent<GhostTowerVisualizer>();

        if (ghostVisualizer == null)
        {
            Debug.LogError("Ghost prefab missing GhostTowerVisualizer!");
            return;
        }

        // Initialize visuals using TurretData
        ghostVisualizer.Initialize(selectedTurret);
    }

    void Update()
    {
        if (!ghostTurret)
            return;

        FollowMouse();

        bool canPlace = CanPlaceAt(
            ghostTurret.transform.position,
            selectedTurret.towerGrid
        );

        // Update grid visual
        ghostVisualizer.SetGridValid(canPlace, validSprite, invalidSprite);

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
        GameObject towerObj = Instantiate(
            selectedTurret.turretPrefab,
            ghostTurret.transform.position,
            Quaternion.identity
        );

        // Initialize with TurretData
        towerObj.GetComponent<Tower>().Initialize(selectedTurret);

        // Set blocking layer
        SetLayerRecursively(towerObj, LayerMask.NameToLayer("PlacementBlocker"));

        ClearPlacement();
    }

    void ClearPlacement()
    {
        if (ghostTurret)
            Destroy(ghostTurret);

        ghostTurret = null;
        selectedTurret = null;
        ghostVisualizer = null;
    }

    bool CanPlaceAt(Vector3 pos, Vector2 size)
    {
        return !Physics2D.OverlapBox(pos, size, 0f, placementLayer);
    }

    void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
            SetLayerRecursively(child.gameObject, layer);
    }
}