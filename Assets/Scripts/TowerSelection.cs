using UnityEngine;

public class TowerSelection : MonoBehaviour
{
    [SerializeField] private GameObject rangeIndicator;
    [SerializeField] private Transform uiAnchor;

    public Transform UIAnchor => uiAnchor;

    public bool IsSelected { get; private set; }

    private Tower tower;

    private void Awake()
    {
        tower = GetComponent<Tower>();

        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);
    }

    public void Select()
    {
        IsSelected = true;

        if (rangeIndicator != null)
            rangeIndicator.SetActive(true);

        TowerSelectedUI.Instance.Show(tower, uiAnchor);
    }

    public void Deselect()
    {
        IsSelected = false;

        if (rangeIndicator != null)
            rangeIndicator.SetActive(false);

        TowerSelectedUI.Instance.Hide();
    }
}