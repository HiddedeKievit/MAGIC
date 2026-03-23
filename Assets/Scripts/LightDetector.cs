using System.Runtime.InteropServices;
using UnityEngine;

public class LightDetector : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] bool lightDetected;


    //sets lightDetected to false at start.
    void Start()
    {
        lightDetected = false;
    }

    //Check to see if lightDetected is true, Debug purposes only.
    void Update()
    {
        if (lightDetected == true)
        {
            print("I'M IN THE SPOTLIGHT BABYYYY");
            
        }
        print("Light detected: " + lightDetected);
    }

    //Sets lightDetected to true if a GameObject with the Lantern tag enters the collision area.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lantern")
        {
            lightDetected = true;
            print("light detected");
            GetComponent<TurretData>().damage += 1;
        }
    }

    //Sets lightDetected to false if a GameObject with the Lantern tag leaves the collision area.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Lantern")
        {
            lightDetected = false;
            GetComponent<TurretData>().damage -= 1;
        }
    }


}
