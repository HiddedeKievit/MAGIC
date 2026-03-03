using UnityEngine;

public class FloatUI : MonoBehaviour
{
    public float amplitude = 10f;   // how high it moves
    public float frequency = 1f;    // speed

    private RectTransform rectTransform;
    private float startY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        rectTransform.anchoredPosition =
            new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}