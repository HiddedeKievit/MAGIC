using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    public static TowerSelection Current;

    public bool IsSelected { get; private set; }

    void OnMouseDown()
    {
        Select();
    }

    void Select()
{
    if (Current == this)
        return;

    if (Current != null)
        Current.Deselect();

    Current = this;
    IsSelected = true;

    TowerSelectedUI.Instance.Show(GetComponent<Tower>());
}

    public void Deselect()
    {
        IsSelected = false;

        if (Current == this)
            Current = null;

        TowerSelectedUI.Instance.Hide();
    }
}