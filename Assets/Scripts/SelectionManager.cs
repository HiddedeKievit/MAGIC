using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if we clicked something
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // If we didn't hit a tower → deselect
            if (hit.collider == null || !hit.collider.GetComponent<TowerSelection>())
            {
                if (TowerSelection.Current != null)
                    TowerSelection.Current.Deselect();
            }
        }
    }
}