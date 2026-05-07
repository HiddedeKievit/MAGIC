using UnityEngine;

public class TowerPanelToggle : MonoBehaviour
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private float openX = 898.12f;
    [SerializeField] private float closedX = 1233f;
    [SerializeField] private GameObject openArrowButton;
    [SerializeField] private GameObject closeArrowButton;
    [SerializeField] private GameObject cardContainer;
    [SerializeField] private float speed = 10f;

    private Vector2 openPosition;
    private Vector2 closedPosition;
    private Vector2 targetPosition;

    private void Awake()
    {
        // Use whatever Y the panel currently has, only swap X for open/closed
        float y = panel.anchoredPosition.y;
        openPosition = new Vector2(openX, y);
        closedPosition = new Vector2(closedX, y);
    }

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
        if (Vector2.Distance(panel.anchoredPosition, targetPosition) < 0.1f)
        {
            panel.anchoredPosition = targetPosition;
            return;
        }

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