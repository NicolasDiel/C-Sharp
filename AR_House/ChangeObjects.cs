using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeObjects : MonoBehaviour
{
    [SerializeField] GameObject ArSessionOrigin;
    [SerializeField] Camera arCamera;
    public GameObject housePrefab;

    public GameObject UiObjects;
    public GameObject UiShopping;
    public GameObject NavigationUI;

    private string activeChair = "Chair0";
    private string showingChair = "Chair0";


    [HideInInspector] public List<GameObject> objectsToChange = new List<GameObject>();
    public Text txtDebug;

    private void Update()
    {
        if (NavigationUI.activeSelf)
        {
            if (objectsToChange.Count == 0)
            {
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Interactable"))
                    {
                        objectsToChange.Add(obj);
                        obj.SetActive(false);
                    }

                ChangeChair("Chair0");
            }
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (IsPointerOverUIObject())
                    return;

                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    txtDebug.text = hitObject.collider.gameObject.tag;
                    if (hitObject.collider.gameObject.tag.Equals("Interactable"))
                    {
                        EnableUiObjects();
                        DisableUIShopping();
                    }
                }
            }
        }

    }

    public void CancelChangeChair()
    {

        ChangeChair(activeChair);

        Debug.Log("ActiveChair = " + activeChair + "  ShowingChair = " + showingChair);
    }

    public void ConfirmActiveChair()
    {
        activeChair = showingChair;

        Debug.Log("ActiveChair = " + activeChair + "  ShowingChair = " + showingChair);
    }

    public void ChangeChair(string name)
    {
        for (int i = 0; i < objectsToChange.Count; i++)
        {
            if (objectsToChange[i].name == name)
            {
                objectsToChange[i].SetActive(true);
                showingChair = objectsToChange[i].name;
            }
            else
            {
                objectsToChange[i].SetActive(false);
            }
        }

    }   

    public void EnableUiObjects()
    {
        UiObjects.SetActive(true);
    }

    public void DisableUiObjects()
    {
        UiObjects.SetActive(false);
    }

    public void DisableUIShopping()
    {
        UiShopping.SetActive(false);
    }


    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        if (results.Count > 0)
        { 
            foreach (var item in results)
            {
                if (item.gameObject.tag == "UI")
                    return true;
            }
        }

        return false;

    }

}
