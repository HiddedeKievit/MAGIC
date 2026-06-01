using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    private TowerSelection currentSelection;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (EventSystem.current != null &&
            EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 mousePos =
            Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit =
            Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
        {
            DeselectCurrent();
            return;
        }

        TowerSelection tower =
    hit.collider.GetComponentInParent<TowerSelection>();

    if (hit.collider != null)
{
    Debug.Log("Hit: " + hit.collider.name);
}

        if (tower == null)
        {
            DeselectCurrent();
            return;
        }

        SelectTower(tower);
    }

    public void SelectTower(TowerSelection tower)
    {
        if (currentSelection == tower)
            return;

        if (currentSelection != null)
            currentSelection.Deselect();

        currentSelection = tower;
        currentSelection.Select();
    }

    public void DeselectCurrent()
    {
        if (currentSelection == null)
            return;

        currentSelection.Deselect();
        currentSelection = null;
    }
}