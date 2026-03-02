using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float hoverScale = 1.06f;
    [SerializeField] private float pressScale = 0.98f;
    [SerializeField] private float speed = 14f;

    private Vector3 _baseScale;
    private Vector3 _targetScale;

    private void Awake()
    {
        _baseScale = transform.localScale;
        _targetScale = _baseScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _targetScale, Time.unscaledDeltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
        => _targetScale = _baseScale * hoverScale;

    public void OnPointerExit(PointerEventData eventData)
        => _targetScale = _baseScale;

    public void OnPointerDown(PointerEventData eventData)
        => _targetScale = _baseScale * pressScale;

    public void OnPointerUp(PointerEventData eventData)
        => _targetScale = _baseScale * hoverScale;
}
