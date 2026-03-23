using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Image image; // optional for color change

    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        if (image != null)
            image.color = Color.white; // brighter
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;

        if (image != null)
            image.color = new Color(0.9f, 0.9f, 0.9f); // slightly dim
    }
}