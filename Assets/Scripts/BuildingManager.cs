using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    [Header("Turret Unlocks")]
    [SerializeField] private List<TurretData> unlockedTurrets = new();
    [SerializeField] private int maxTurrets = 6;

    public IReadOnlyList<TurretData> UnlockedTurrets => unlockedTurrets;

    public TurretData SelectedTurret { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UnlockTurret(TurretData turret)
    {
        if (unlockedTurrets.Count >= maxTurrets)
            return;

        if (!unlockedTurrets.Contains(turret))
            unlockedTurrets.Add(turret);
    }

    public void SelectTurret(TurretData turret)
    {
        SelectedTurret = turret;
        Debug.Log("Selected turret: " + turret.turretName);
    }

    public void ClearSelection()
    {
        SelectedTurret = null;
    }
}