using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject glowObject;

    private void Awake()
    {
        if (glowObject) glowObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (glowObject) glowObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (glowObject) glowObject.SetActive(false);
    }
}
