using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour
{
    [SerializeField] private Image icon;

    private TurretData data;

    public void Setup(TurretData turretData)
    {
        data = turretData;
        icon.sprite = turretData.icon;
    }

    public void OnClick()
    {
        Debug.Log("Selected turret: " + data.turretName);
        // Later: enter placement mode
    }
}