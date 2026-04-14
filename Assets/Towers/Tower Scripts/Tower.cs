using UnityEngine;

public class Turret : MonoBehaviour
{
    public TurretData data;

    [HideInInspector] public TowerTargeting targeting;
    [HideInInspector] public TowerShooting shooting;

    private void Awake()
    {
        targeting = GetComponent<TowerTargeting>();
        shooting = GetComponent<TowerShooting>();

        targeting.Initialize(this);
        shooting.Initialize(this);
    }
}