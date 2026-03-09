using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    public bool IsSelected { get; private set; }

    private void OnMouseDown()
    {
        IsSelected = !IsSelected;
    }

    public void Deselect()
    {
        IsSelected = false;
    }
}