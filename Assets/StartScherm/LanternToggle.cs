using UnityEngine;
using UnityEngine.UI;

public class LanternToggle : MonoBehaviour
{
    [Header("Sprites")]
    public Image lanternImage;
    public Sprite closedLantern;
    public Sprite openLantern;

    [Header("Bonfire")]
    public BonFire bonFire; // drag the Bonfire (parent) that has the BonFire script

    [Header("Light Objects")]
    public GameObject lightBeam;   // the big beam sprite
    public GameObject lanternGlow; // optional small glow around lantern

    [Header("Ghost Reaction")]
    public Image ghostImage;
    public Color ghostLitColor = Color.white;
    public Color ghostDarkColor = new Color(0.85f, 0.85f, 0.85f, 1f);

    [Header("Fire FX")]
    public GameObject fireBurst; // the moving flame (plays once)
    public GameObject firePatch; // optional loop on grass

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

        if (lanternImage)
            lanternImage.sprite = isOn ? openLantern : closedLantern;

        if (lightBeam) lightBeam.SetActive(isOn);
        if (lanternGlow) lanternGlow.SetActive(isOn);

        if (ghostImage)
            ghostImage.color = isOn ? ghostLitColor : ghostDarkColor;

        if (isOn)
        {
            // Ignite bonfire (auto returns to wood after burnTime in BonFire script)
            if (bonFire) bonFire.Ignite();

            // Play burst when opening
            if (fireBurst)
            {
                fireBurst.SetActive(true);

                Animator anim = fireBurst.GetComponent<Animator>();
                if (anim)
                    anim.Play(0, 0, 0f); // restart from beginning
            }

            // Optional: enable loop patch while lantern is on
            if (firePatch) firePatch.SetActive(true);
        }
        else
        {
            // Optional: if lantern closes, hide these FX
            if (fireBurst) fireBurst.SetActive(false);
            if (firePatch) firePatch.SetActive(false);

            // If you want the bonfire to STOP immediately when lantern closes,
            // uncomment this line (otherwise it will finish its burnTime):
            // if (bonFire) bonFire.ExtinguishImmediate();
        }
    }
}