using UnityEngine;

public class TowerPanelToggle : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private Vector2 openPosition;
    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private GameObject openArrowButton;
    [SerializeField] private GameObject closeArrowButton;
    [SerializeField] private GameObject cardContainer;
    [SerializeField] private float speed = 10f;

    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = closedPosition;
        panel.anchoredPosition = closedPosition;

        cardContainer.SetActive(false);

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
        cardContainer.SetActive(true);
        targetPosition = openPosition;

        openArrowButton.SetActive(false);
        closeArrowButton.SetActive(true);
    }

    public void ClosePanel()
    {
        targetPosition = closedPosition;
        cardContainer.SetActive(false);

        openArrowButton.SetActive(true);
        closeArrowButton.SetActive(false);
    }
}