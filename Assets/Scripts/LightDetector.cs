using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    [SerializeField] Collider2D Collider;
    [SerializeField] bool lightDetected;
    [SerializeField] GameObject projectile;

[SerializeField] GameObject effect;
    private Tower tower;



    //sets lightDetected to false at start.
    void Start()
    {
        lightDetected = false;
        tower = GetComponent<Tower>();
        effect.SetActive(false);
    }

    //Check to see if lightDetected is true, Debug purposes only.
    void Update()
    {
        if (lightDetected == true)
        {
            print("I'M IN THE SPOTLIGHT BABYYYY");
        }
    }

    //Sets lightDetected to true if a GameObject with the Lantern tag enters the collision area.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lantern")
        {
            lightDetected = true;
            //particles
            effect.SetActive(true);
            projectile.GetComponent<ProjectileData>().Damage += 1;
            print("Turret damage increased to " + projectile.GetComponent<ProjectileData>().Damage);
            if (tower != null)
                tower.RefreshDamage();
        }
    }

    //Sets lightDetected to false if a GameObject with the Lantern tag leaves the collision area.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Lantern")
        {
            lightDetected = false;
            //no more particles
            effect.SetActive(false);
            projectile.GetComponent<ProjectileData>().Damage -= 1;
            print("Turret damage decreased to " + projectile.GetComponent<ProjectileData>().Damage);
            if (tower != null)
                tower.RefreshDamage();
        }
    }


}
