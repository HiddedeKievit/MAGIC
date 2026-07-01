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
    [SerializeField] private TextMeshProUGUI txt_sellTower;

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
            txt_sellTower.text = $"Sell for {tower.cost} Mana.";
        }
    }

    public void SetTowerToView(TowerData towerToView)
    {
        tower = towerToView;
        gameObject.SetActive(towerToView != null);
    }

    public bool IsTower(TowerData checkTower)
    {
        return tower == checkTower;
    }

    //// Menu Buttons

    // next tower priority button
    public void NextPriority()
    {
        if (tower != null)
        {
            int count = Enum.GetNames(typeof(Targeting)).Length;
            tower.targeting = (Targeting)( ( (int)tower.targeting + 1 ) % count );
        }
    }

    // previous priority button
    public void PrevPriority()
    {
        if (tower != null)
        {
            int count = Enum.GetNames(typeof(Targeting)).Length;
            tower.targeting = (Targeting)( ( (int)tower.targeting - 1 + count ) % count );
        }
    }

    // close the menu button
    public void CloseMenu()
    {
        SetTowerToView(null);
    }

    public void SellTower()
    {
        TowerManager.Instance.SellTower(tower);

        CloseMenu();
    }
}
