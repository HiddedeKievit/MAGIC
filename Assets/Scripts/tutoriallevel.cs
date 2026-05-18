using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialLevel : MonoBehaviour
{ public GameObject[] popups;
private int PopupIndex;
public Scene tutoriallevel;
public GameObject wavespawner;
public PlacementManager ghostplace;
public GameObject manamanager;

public float waittime = 3f;
    void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if (i == PopupIndex)
            {
                popups[i].SetActive(true);
            }
            else
            {
                popups[i].SetActive(false);
            }
        }
        if (PopupIndex == 0 )
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            waittime = 3f;
            }

                
        }
        else if (PopupIndex == 1)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
            
        }
                else if (PopupIndex == 2)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
            
        }
        else if (PopupIndex == 3)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
            
        }
        else if (PopupIndex == 4 )
        {
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
                }
        else if (PopupIndex == 5 )
        {
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
                }
        else if (PopupIndex == 6 )
        {
                        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
            PopupIndex++;
            }
                }
        else if (PopupIndex == 7)
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
}}