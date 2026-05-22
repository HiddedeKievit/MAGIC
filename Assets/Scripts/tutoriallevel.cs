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
    public GameObject tower2;
    public GameObject tower3;
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
            }
        } else if (PopupIndex == 8)
        {
            
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                
                PopupIndex++;
                print("PopupIndex is now: " + PopupIndex);
                openbutton.SetActive(true);

            }
        } else if (PopupIndex == 9 )
        {
            
            
            if (closearrowbutton.activeSelf == true)
            {
                PopupIndex++;
                
            }
        } else if (PopupIndex == 10 )
        
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
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
            if (wavespawner.activeInHierarchy == false)
            {
                wavespawner.SetActive(true);

            }
            if (manamanager.activeInHierarchy == false)
            {
                manamanager.SetActive(true);

            }
            if (tower2.activeInHierarchy == false)
            {
                tower2.SetActive(true);

            }
            if (tower3.activeInHierarchy == false)
            {
                tower3.SetActive(true);

            }

    }
    }
}