using UnityEngine;

public class TowerPanelToggle : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Vector2 openPosition;
    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private GameObject openArrowButton;
    [SerializeField] private GameObject closeArrowButton;
    [SerializeField] private float speed = 10f;
    [SerializeField] private TurretMenu turretMenu;

    private Vector2 targetPosition;
    private bool isOpen = false;

    private void Start()
    {
        targetPosition = closedPosition;
        panel.anchoredPosition = closedPosition;

        openArrowButton.SetActive(true);
        closeArrowButton.SetActive(false);
    }

    private void Update()
    {
        panel.anchoredPosition = Vector2.Lerp(
            panel.anchoredPosition,
            targetPosition,
            Time.unscaledDeltaTime * speed
        );
    }

    public void OpenPanel()
    {
        isOpen = true;
        targetPosition = openPosition;

        openArrowButton.SetActive(false);
        closeArrowButton.SetActive(true);

        if (turretMenu != null)
            turretMenu.PopulateMenu();
    }

    public void ClosePanel()
    {
        isOpen = false;
        targetPosition = closedPosition;

        openArrowButton.SetActive(true);
        closeArrowButton.SetActive(false);
    }
}