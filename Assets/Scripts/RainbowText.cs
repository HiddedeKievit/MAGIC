using UnityEngine;
using TMPro;

public class RainbowLetter : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private float hueOffset = 0f;
    [SerializeField] private float saturation = 0.55f;
    [SerializeField] private float value = 1f;

    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (tmp == null) return;

        float hue = Mathf.Repeat(Time.unscaledTime * speed + hueOffset, 1f);
        Color c = Color.HSVToRGB(hue, saturation, value);
        tmp.color = c;
    }

    public void Setup(float newOffset, float newSpeed, float newSaturation, float newValue)
    {
        hueOffset = newOffset;
        speed = newSpeed;
        saturation = newSaturation;
        value = newValue;
    }
}