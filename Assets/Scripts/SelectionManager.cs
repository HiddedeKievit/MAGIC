using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Ignore clicks on UI (like the Sell button)
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            // If we didn't hit a tower → deselect
            if (hit == null || !hit.GetComponent<TowerSelection>())
            {
                if (TowerSelection.Current != null)
                    TowerSelection.Current.Deselect();
            }
        }
    }
}