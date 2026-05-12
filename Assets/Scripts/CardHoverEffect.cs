using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover")]
    [SerializeField] private float hoverScale = 1.08f;
    [SerializeField] private float hoverMoveY = 12f;
    [SerializeField] private float hoverRotation = 2f;
    [SerializeField] private float speed = 10f;

    [Header("Visuals")]
    [SerializeField] private Image cardImage;
    

    private RectTransform rectTransform;

    private Vector3 originalScale;
    private Vector2 originalPosition;
    private Quaternion originalRotation;

    private Vector3 targetScale;
    private Vector2 targetPosition;
    private Quaternion targetRotation;

    private bool isSelected = false;
    private bool isHovering = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        originalScale = transform.localScale;
        originalPosition = rectTransform.anchoredPosition;
        originalRotation = transform.localRotation;

        targetScale = originalScale;
        targetPosition = originalPosition;
        targetRotation = originalRotation;

        
    }

    private void Update()
    {
        float delta = Time.unscaledDeltaTime;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, delta * speed);
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, delta * speed);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, delta * speed);

        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;

        targetScale = originalScale * hoverScale;
        targetPosition = originalPosition + new Vector2(0, hoverMoveY);
        targetRotation = Quaternion.Euler(0, 0, hoverRotation);

        if (cardImage != null)
            cardImage.color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;

        targetScale = originalScale;
        targetPosition = originalPosition;
        targetRotation = originalRotation;

        if (cardImage != null && !isSelected)
            cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isSelected = true;

        if (cardImage != null)
            cardImage.color = Color.white;

        Debug.Log("Selected card: " + gameObject.name);
    }

    public void DeselectCard()
    {
        isSelected = false;

        if (cardImage != null && !isHovering)
            cardImage.color = new Color(0.9f, 0.9f, 0.9f);
    }
}