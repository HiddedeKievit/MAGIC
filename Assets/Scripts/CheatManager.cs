using UnityEngine;

public class CheatManager : MonoBehaviour
{
    public bool CheatsOn;
    public ManaManager manaManager;

    void Start()
    {
        CheatsOn = false;
    }

    public void CheatEnabler()
    {
        CheatsOn = true;
    }

    void Update()
    {
        if (CheatsOn == true)
        {
            manaManager.mana = 9999999;
        }
    }
}
