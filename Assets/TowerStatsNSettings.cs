using System;
using TMPro;
using UnityEngine;

public class TowerStatsNSettings : MonoBehaviour
{

    TowerData tower = null;

    public static TowerStatsNSettings Instance;

    [SerializeField] private TextMeshProUGUI txt_damage;
    [SerializeField] private TextMeshProUGUI txt_kills;
    [SerializeField] private TextMeshProUGUI txt_priority;

    private void Start()
    {
        if (!Instance)
            Instance = this;

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (tower)
        {
            txt_priority.text = $"Priority: {tower.targeting}";
            txt_damage.text = $"Damage Dealt: {tower.damageDealt}";
            txt_kills.text = $"Kills: {tower.kills}";
        }
    }

    public void SetTowerToView(TowerData towerToView)
    {
        tower = towerToView;
        gameObject.SetActive(towerToView != null);
    }

    public void nextPriority()
    {
        if (tower != null)
        {
            int count = Enum.GetNames(typeof(Targeting)).Length;
            tower.targeting = (Targeting)( ( (int)tower.targeting + 1 ) % count );
        }
    }

    public void prevPriority()
    {
        if (tower != null)
        {
            int count = Enum.GetNames(typeof(Targeting)).Length;
            tower.targeting = (Targeting)( ( (int)tower.targeting - 1 + count ) % count );
        }
    }

    public bool IsTower(TowerData checkTower)
    {
        return tower == checkTower;
    }
}
