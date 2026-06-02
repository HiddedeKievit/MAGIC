using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;
    [SerializeField] private LayerMask towerLayer;

    private TowerSelection currentSelection;

    private void Awake()
    {
        Instance = this;
        Debug.Log("[SelectionManager] Awake");
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

         Debug.Log("[SelectionManager] Left click detected");

        // if (EventSystem.current != null &&
        //     EventSystem.current.IsPointerOverGameObject())
        // {
        //     Debug.Log("[SelectionManager] Clicked UI, ignoring");
        //     return;
        // }

        Vector2 mousePos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log($"[SelectionManager] Mouse World Pos: {mousePos}");

        RaycastHit2D hit =
        Physics2D.Raycast(
        mousePos,
        Vector2.zero,
        Mathf.Infinity,
        towerLayer
    );

        if (hit.collider == null)
        {
            Debug.Log("[SelectionManager] Raycast hit NOTHING");

            DeselectCurrent();
            return;
        }

        Debug.Log($"[SelectionManager] Hit collider: {hit.collider.name}");

        TowerSelection tower =
            hit.collider.GetComponentInParent<TowerSelection>();

        if (tower == null)
        {
            Debug.Log("[SelectionManager] Collider has NO TowerSelection");

            DeselectCurrent();
            return;
        }

        Debug.Log($"[SelectionManager] Found TowerSelection on {tower.name}");

        SelectTower(tower);
    }

    public void SelectTower(TowerSelection tower)
    {
        Debug.Log($"[SelectionManager] SelectTower({tower.name})");

        if (currentSelection == tower)
        {
            Debug.Log("[SelectionManager] Already selected");
            return;
        }

        if (currentSelection != null)
        {
            Debug.Log($"[SelectionManager] Deselecting previous tower: {currentSelection.name}");
            currentSelection.Deselect();
        }

        currentSelection = tower;

        Debug.Log($"[SelectionManager] Selecting tower: {tower.name}");

        currentSelection.Select();
    }

    public void DeselectCurrent()
    {
        if (currentSelection == null)
        {
            Debug.Log("[SelectionManager] No tower selected");
            return;
        }

        Debug.Log($"[SelectionManager] Deselecting {currentSelection.name}");

        currentSelection.Deselect();
        currentSelection = null;
    }
}