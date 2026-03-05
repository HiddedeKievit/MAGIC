using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternManager : MonoBehaviour
{

    private Camera mainCamera;

    [SerializeField] Light2D lantern; 
    [SerializeField] private bool lightFollowing;
    [SerializeField] LanternLevel level;
    [SerializeField] CircleCollider2D circleCollider;

    enum LanternLevel { One, Two, Three}
    
    void Start()
    {
        lightFollowing = true;
        mainCamera = Camera.main;
    }

    void Update()
    {
        //Changes lightFollowing value when pressing spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (lightFollowing)
            {
                case false:
                    lightFollowing = true;
                    break;

                case true:
                    lightFollowing = false;
                    break;
            }
        }

        //Calls on FollowMousePosition() to set the Objects location to the mouse if lightFollowing is set to true.
        if (lightFollowing == true)
        {
            FollowMousePosition();
        }

        //These lines change LanternLevel by pressing 1, 2 or 3, changing the light radius.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            level = LanternLevel.One;
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            level = LanternLevel.Two;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            level = LanternLevel.Three;
        }

        //Changes light radius
        UpgradeLantern();
    }

    //Gets the mouse position from GetWorldPositionFromMouse() and sets the Objects location to those coordinates.
    private void FollowMousePosition()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    //Look through the camera at the position of the mouse and returns these coordinates.
    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    //Increases radius of the Light when the LanternLevel increases.
    private void UpgradeLantern()
    {
        switch (level)
        {
            case LanternLevel.One:
                lantern.pointLightInnerRadius = 1;
                lantern.pointLightOuterRadius = 2;
                break;

            case LanternLevel.Two:
                lantern.pointLightInnerRadius = 1;
                lantern.pointLightOuterRadius = 3;
                break;

            case LanternLevel.Three:
                lantern.pointLightInnerRadius = 2;
                lantern.pointLightOuterRadius = 4;
                break;
        }
    }

}
