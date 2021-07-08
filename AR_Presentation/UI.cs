using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UI : MonoBehaviour
{
    public GameObject ContentScreen;   

    public GameObject CurrentGameObject;
    private GameObject arObject;

    [SerializeField] private GameObject backButton;

    public GameObject[] presentations;

    private GameObject PowerPoint;


    // Start is called before the first frame update
    void Awake()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    }

    
    public void MaximizeVideo(VideoClip clip, GameObject calledBy)
    {
        arObject = GameObject.FindGameObjectWithTag("Avatar");
        arObject.SetActive(false);

        if (backButton != null)
            backButton.SetActive(false);

        ContentScreen.SetActive(true);
        ContentScreen.GetComponent<VideoPlayer>().enabled = true;
        ContentScreen.GetComponent<VideoPlayer>().clip = clip;       
        CurrentGameObject = calledBy;
    }

    public void MinimizeVideo()
    {
        arObject.SetActive(true);

        if (backButton != null)
            backButton.SetActive(true);

        CurrentGameObject.transform.GetChild(0).GetComponent<VideoPlayer>().enabled = true;


        ContentScreen.GetComponent<VideoPlayer>().enabled = false;
        CurrentGameObject = null;


        ContentScreen.SetActive(false);
    }

    public void ActivatePowerPoint(GameObject calledBy, int number)
    {
        arObject = GameObject.FindGameObjectWithTag("Avatar");
        arObject.SetActive(false);

        if (backButton != null)
            backButton.SetActive(false);

        PowerPoint = presentations[number];

        PowerPoint.SetActive(true);

        CurrentGameObject = calledBy;
    }

    public void DeactivatePowerPoint()
    {
        arObject.SetActive(true);

        if (backButton != null)
            backButton.SetActive(true);


        PowerPoint.SetActive(false);
        CurrentGameObject = null;
    }


}
