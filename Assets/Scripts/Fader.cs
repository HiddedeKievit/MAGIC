using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Fader : MonoBehaviour
{
    public TMP_Text regionName;

    private float fadeTime;
    private bool fadingIn;
    // Start is called before the first frame update
    void Start()
    {
        regionName.CrossFadeAlpha(0, 0.0f, false);
        fadeTime = 0;
        fadingIn = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            FadeIn();
        }
        else if (regionName.color.a != 0)
        {

            regionName.CrossFadeAlpha(0, 0.5f, false);
        }
    }
    // Fades in the region name when the player enters a new region, and then fades it out after a short delay
    void FadeIn()
    {
        fadeTime += Time.deltaTime;
        regionName.CrossFadeAlpha(1, 0.5f, false);
        if (regionName.color.a == 1 && fadeTime >= 1.5f)
        {
            fadingIn = false;
            fadeTime = 0;
        }
    }

    // When the player enters a region, start fading in the region name
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Region")
        {
            fadingIn = true;
            regionName.text = other.name;
        }
    }
}

