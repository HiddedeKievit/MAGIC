using UnityEngine;

public class BuildMenuToggle : MonoBehaviour
{
    [SerializeField] private GameObject turretMenu;

    private bool isOpen;

    void Start()
    {
        turretMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        isOpen = !isOpen;
        turretMenu.SetActive(isOpen);
    }
}