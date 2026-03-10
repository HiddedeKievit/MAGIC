using UnityEngine;

public class Tower : MonoBehaviour
{
    public TurretData Data { get; private set; }

    [SerializeField] private Transform uiAnchor;
    public Transform UIAnchor => uiAnchor;

    public void Initialize(TurretData data)
    {
        Data = data;
    }
}