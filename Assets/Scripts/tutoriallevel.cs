using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour
{
    public GameObject[] popups;
    private int PopupIndex;
    public Scene tutoriallevel;
    public GameObject wavespawner;
    public PlacementManager ghostplace;
    public GameObject manamanager;

    public TowerPanelToggle towerPanelToggle;
    public GameObject clicktocontinue;

    public GameObject closearrowbutton;
    public SpriteRenderer GhostTower;

    public Button openbutton;
    public Button tower2;
    public Button tower3;
    void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if (i == PopupIndex)
            {
                popups[i].SetActive(true);
            } else
            {
                popups[i].SetActive(false);
            }
        }
        if (PopupIndex == 0)
        {
            // Disable tower buttons at the start of the tutorial
            openbutton.interactable = false;
            tower2.interactable = false;
            tower3.interactable = false;

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                // Move to the next popup when the player clicks anywhere
                PopupIndex++;
            
            }


        } else if (PopupIndex == 1)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }
        
        } else if (PopupIndex == 2)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }        
        
        } else if (PopupIndex == 3)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }
        } else if (PopupIndex == 4)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }

        } else if (PopupIndex == 5)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }
        } else if (PopupIndex == 6)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            }
        } else if (PopupIndex == 7)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {

                PopupIndex++;
            }
        } else if (PopupIndex == 8)
        {
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                
                PopupIndex++;
                print("PopupIndex is now: " + PopupIndex);
                //make sure you can open the tower menu
                openbutton.interactable = true;

            }
        } else if (PopupIndex == 9 )
        {
            
            // Wait for the player to click the open arrow button before moving to the next popup
            if (closearrowbutton.activeSelf == true)
            {
                PopupIndex++;
                
            }
        } else if (PopupIndex == 10 )
        
        {
            // check if the player is trying to place a tower
            if (GhostTower.sprite != null)
            {
                PopupIndex++;
            
            }
                    } else if (PopupIndex == 11 )
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            if (clicktocontinue.activeInHierarchy == true)
            {
                clicktocontinue.SetActive(false);
            }
            }
        } else if (PopupIndex == 12 )

        
        {
            // Wait for the player to click anywhere to continue, then enable the wave spawner, mana manager, tower buttons and start the level
            if (wavespawner.activeInHierarchy == false)
            {
                wavespawner.SetActive(true);

            }
            if (manamanager.activeInHierarchy == false)
            {
                manamanager.SetActive(true);

            }
            if (tower2.interactable == false)
            {
                tower2.interactable = true;

            }
            if (tower3.interactable == false)
            {
                tower3.interactable = true;

            }

    }
    }
}