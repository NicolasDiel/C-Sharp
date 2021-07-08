using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPoint : MonoBehaviour
{
    public GameObject testNotification;
    public GameObject image;

    [SerializeField] private Sprite[] presentation;

    [SerializeField] private GameObject previousButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject infoButton;
    [SerializeField] private TMPro.TextMeshProUGUI popUpText;

    private Sprite currentSprite;
    private int pos = 0;

    private void Awake()
    {
        if (image != null && presentation != null)
        {
            ChangeImage(0);
        }

        popUpText.text = UserData.GetUserName() + ", we’ve send you an email!";

    }

    private void Start()
    {
        testNotification.GetComponent<NotificationManager>().ExecuteNotification();
    }

    public void NextPage()
    {
        if (presentation == null)
            return;

        if (currentSprite == presentation[presentation.Length-1])
            return;

        ChangeImage(pos + 1);
    }
    
    public void PreviousPage()
    {
        if (presentation == null)
            return;

        if (currentSprite == presentation[0])
            return;

        ChangeImage(pos - 1);
    }

    public void ChangeImage(int position)
    {
        image.GetComponent<Image>().sprite = presentation[position];
        currentSprite = presentation[position];
        pos = position;

        UpdateButtonsState();
    }

    private void UpdateButtonsState()
    {
        //Update buttons state
        if (pos == 0)
        {
            previousButton.SetActive(false);
            nextButton.SetActive(true);
            infoButton.SetActive(false);
        }
        else if (pos == presentation.Length - 1)
        {
            previousButton.SetActive(true);
            nextButton.SetActive(false);
            infoButton.SetActive(true);
        }
        else
        {
            previousButton.SetActive(true);
            nextButton.SetActive(true);
            infoButton.SetActive(false);
        }
    }
}
