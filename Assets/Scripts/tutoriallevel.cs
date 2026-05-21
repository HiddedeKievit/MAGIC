using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject openbutton;
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
            openbutton.SetActive(false);

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
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
                print("PopupIndex is now: " + PopupIndex);

            }
        } else if (PopupIndex == 8 )
        {
            openbutton.SetActive(true);
            
            if (closearrowbutton.activeSelf == true)
            {
                PopupIndex++;
                
            }
        } else if (PopupIndex == 9 )
        
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            
            }
                    } else if (PopupIndex == 10 )
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                PopupIndex++;
            if (clicktocontinue.activeInHierarchy == true)
            {
                clicktocontinue.SetActive(false);
            }
            }
        } else if (PopupIndex == 11 )

        
        {
            if (wavespawner.activeInHierarchy == false)
            {
                wavespawner.SetActive(true);

            }
            if (manamanager.activeInHierarchy == false)
            {
                manamanager.SetActive(true);

            }

    }
    }
}