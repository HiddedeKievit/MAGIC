using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    public TurretData turretData;
    public PlacementManager placementManager;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Setup()
    {
        if (icon != null && turretData != null)
            icon.sprite = turretData.icon;
    }

    public void OnClick()
    {
        if (placementManager != null && turretData != null)
            placementManager.StartPlacement(turretData);
    }
}