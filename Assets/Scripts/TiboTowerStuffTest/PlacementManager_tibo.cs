using UnityEngine;

public class PlacementManager_tibo : MonoBehaviour
{
    public static PlacementManager_tibo Instance;

    public SpriteRenderer GhostTower;

    [SerializeField] private SpriteRenderer ghostRangeIndicator;
    [SerializeField] private Sprite blueRangeSprite;
    [SerializeField] private Sprite redRangeSprite;

    // Layers
    [SerializeField] private LayerMask placementLayer;
    [SerializeField] private LayerMask waterPlacementLayer;

    public TowerData TowerToPlace;
    private bool canPlace = false;

    // the colors to use for when you can and cant place a tower.
    private Color clr_cantplace = new(1f, 0.25f, 0.25f, 0.8f);
    private Color clr_canplace = new(0.25f, 1f, 0.25f, 0.8f);

    Vector3 mousePosition = new();

    private void Awake()
    {
        if (Instance)
            return;

        Instance = this;

        if (ghostRangeIndicator != null)
        {
            ghostRangeIndicator.gameObject.SetActive(false);
        }
    }

    public void StartPlacementt(TowerData tower)
    {
        tower.enabled = false;
        TowerToPlace = tower;

        SpriteRenderer towerRenderer =
            tower.GetComponentInChildren<SpriteRenderer>();

        if (towerRenderer == null)
        {
            Debug.LogError(
                $"No SpriteRenderer found on {tower.name} or its children."
            );
            return;
        }

        GhostTower.sprite = towerRenderer.sprite;
        GhostTower.transform.localScale =
            TowerToPlace.transform.localScale;

        SetupGhostRangeIndicator();
    }

    private void SetupGhostRangeIndicator()
    {
        if (TowerToPlace == null || ghostRangeIndicator == null)
            return;

        ghostRangeIndicator.sprite = blueRangeSprite;

        if (ghostRangeIndicator.sprite == null)
        {
            Debug.LogError(
                "Blue Range Sprite is not assigned in PlacementManager!"
            );
            return;
        }

        float spriteWidth =
            ghostRangeIndicator.sprite.bounds.size.x;

        float diameter = TowerToPlace.range * 2f;

        // Compensate for GhostTower scaling
        float parentScale =
            GhostTower.transform.lossyScale.x;

        float scale =
            diameter / spriteWidth / parentScale;

        ghostRangeIndicator.transform.localScale =
            new Vector3(scale, scale, 1f);

        ghostRangeIndicator.transform.localPosition = Vector3.zero;

        ghostRangeIndicator.gameObject.SetActive(true);
    }

    private void Update()
    {
        // check if i have a tower to place.
        if (!TowerToPlace)
            return;

        bool validPlacement;

        if (TowerToPlace.waterTower)
        {
            Vector2 half = TowerToPlace.hitbox / 2f;

            // Optional: shrink the corners inward slightly
            // to avoid issues on collider edges.
            half -= Vector2.one * 0.05f;

            Vector2 pos = GhostTower.transform.position;

            bool topLeft = Physics2D.OverlapPoint(
                pos + new Vector2(-half.x, half.y),
                waterPlacementLayer
            );

            bool topRight = Physics2D.OverlapPoint(
                pos + new Vector2(half.x, half.y),
                waterPlacementLayer
            );

            bool bottomLeft = Physics2D.OverlapPoint(
                pos + new Vector2(-half.x, -half.y),
                waterPlacementLayer
            );

            bool bottomRight = Physics2D.OverlapPoint(
                pos + new Vector2(half.x, -half.y),
                waterPlacementLayer
            );

            validPlacement =
                topLeft &&
                topRight &&
                bottomLeft &&
                bottomRight;
        }
        else
        {
            validPlacement =
                !Physics2D.OverlapBox(
                    GhostTower.transform.position,
                    TowerToPlace.hitbox,
                    0f,
                    placementLayer
                );
        }

        canPlace =
            TowerManager.Instance.GetOverlappingTower(
                GhostTower.transform.position,
                TowerToPlace.hitbox
            ) == null
            && validPlacement
            && ManaManager.Instance.CanAfford(
                TowerToPlace.cost
            );

        if (canPlace)
        {
            GhostTower.color = clr_canplace;

            if (ghostRangeIndicator != null)
            {
                ghostRangeIndicator.sprite = blueRangeSprite;
            }

            // click left button, spend mana and spawn new tower then reset the ghost tower.
            if (Input.GetMouseButtonDown(0))
            {
                ManaManager.Instance.SpendMana(TowerToPlace.cost);

                TowerData newTower =
                    TowerManager.Instance.SpawnTower(
                        TowerToPlace,
                        GhostTower.transform.position
                    );

                newTower.enabled = true;

                GhostTower.sprite = null;
                TowerToPlace = null;

                if (ghostRangeIndicator != null)
                {
                    ghostRangeIndicator.gameObject.SetActive(false);
                }

                return;
            }
        }
        else
        {
            // cant be placed, make ghost tower red
            GhostTower.color = clr_cantplace;

            if (ghostRangeIndicator != null)
            {
                ghostRangeIndicator.sprite = redRangeSprite;
            }
        }

        // rightclick, cancel placement
        if (Input.GetMouseButtonDown(1))
        {
            GhostTower.sprite = null;
            TowerToPlace = null;

            if (ghostRangeIndicator != null)
            {
                ghostRangeIndicator.gameObject.SetActive(false);
            }

            return;
        }

        // if the sprite exists, move the ghost tower to mouse cursor.
        if (GhostTower.sprite)
        {
            mousePosition =
                Camera.main.ScreenToWorldPoint(
                    Input.mousePosition
                );

            mousePosition.z = 0;

            GhostTower.transform.position = mousePosition;
        }
    }
}