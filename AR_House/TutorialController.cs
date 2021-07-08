using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject uiTutorial;   
    public List<GameObject> listUi = new List<GameObject>();
    private bool DisableTutorial = false;

    public GameObject UI_AR;
    public GameObject NavigationUI;

    void Awake()
    {
        foreach (Transform child in uiTutorial.transform)
        {
            listUi.Add(child.gameObject);
        }
    }

    public void SkipTutorial()
    {
        DisableTutorial = true;
    }

    public void NextTutorial(int number)
    {
        if (DisableTutorial == false)
        {
            ShowTutorial(number);
        }
        else if (number == 2)
        {
            UI_AR.SetActive(true);
            NavigationUI.SetActive(true);
        }

    }

    public void ShowTutorial(int number)
    {
        foreach (Transform child in uiTutorial.transform)
        {
            child.gameObject.SetActive(false);
        }

        uiTutorial.SetActive(true);
        listUi[number].SetActive(true);
    }
}

