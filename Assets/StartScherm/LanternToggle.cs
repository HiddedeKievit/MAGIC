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

    public GameObject fireBurst; // the moving flame
    public GameObject firePatch; // the fire on the grass (loop)

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

        if (isOn)
        {
            // Play burst when opening
            if (fireBurst)
            {
                fireBurst.SetActive(true);

                Animator anim = fireBurst.GetComponent<Animator>();
                if (anim)
                {
                    anim.Play(0, 0, 0f); // restart animation from beginning
                }
            }
        }
        else
        {
            // Turn everything off when closing
            if (fireBurst) fireBurst.SetActive(false);
            if (firePatch) firePatch.SetActive(false);
        }
    }
}