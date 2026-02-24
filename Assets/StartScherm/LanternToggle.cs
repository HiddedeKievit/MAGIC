using UnityEngine;
using UnityEngine.UI;

public class LanternToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Image lanternImage;
    public Sprite closedLantern;
    public Sprite openLantern;

    [Header("Light Objects")]
    public GameObject lightBeam;   // the big beam sprite
    public GameObject lanternGlow; // optional small glow around lantern

    [Header("Ghost Reaction")]
    public Image ghostImage;
    public Color ghostLitColor = Color.white;
    public Color ghostDarkColor = new Color(0.85f, 0.85f, 0.85f, 1f);

    private bool isOn = false;

    void Start()
    {
        SetState(false);
    }

    public void Toggle()
    {
        SetState(!isOn);
    }

    void SetState(bool on)
    {
        isOn = on;

        lanternImage.sprite = isOn ? openLantern : closedLantern;

        if (lightBeam) lightBeam.SetActive(isOn);
        if (lanternGlow) lanternGlow.SetActive(isOn);

        if (ghostImage) ghostImage.color = isOn ? ghostLitColor : ghostDarkColor;
    }
}
    