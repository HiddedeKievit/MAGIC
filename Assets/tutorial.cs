using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{ public GameObject[] popups;
private int PopupIndex;
public Scene tutoriallevel;
public GameObject bookopen;
public float waittime = 9f;
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
        if (PopupIndex==0)
        {
            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.A))
            {
                PopupIndex++;
            }
        }
        else if (PopupIndex == 1)
        {
            if (bookopen.activeInHierarchy == true)
            {
                PopupIndex++;
            }
        }
        else if (PopupIndex == 2)
        {
            if (bookopen.activeInHierarchy == false)
            {
                PopupIndex++;
            }
        }
        else if (PopupIndex == 3)
        {
            if (tutoriallevel.name == "LVL 1")
            {
                PopupIndex++;
                PopupIndex = 0;
            }


        }
    } 
}