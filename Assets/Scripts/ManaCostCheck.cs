using UnityEngine;
using UnityEngine.UI;

public class ManaCostCheck : MonoBehaviour
{
    ManaManager manaManager;
    TurretData turretData;
    public Button placementButton;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("CHECKING!!");
        if (manaManager.mana >= turretData.manaCost)
        {
            placementButton.enabled = true;
        } else
        {
            placementButton.enabled = false;
        }
    }
}
