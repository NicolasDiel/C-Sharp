using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField] ARRaycastManager m_RaycastManager;
    [SerializeField] ARPlaneManager m_ARPlaneManager;
    [SerializeField] ARPointCloudManager m_ARPointCloudManager;
    [SerializeField] ARSession m_ARSession;

    public Camera ArCamera;
    public GameObject ObjectToSpawn = null;
    public GameObject ArUI;


    public GameObject arObject = null;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();   

    void Awake()
    {
        GameObject aux = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (arObject == null)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
                    {
                        Pose hitPose = s_Hits[0].pose;

                        arObject = Instantiate(ObjectToSpawn, hitPose.position, hitPose.rotation);

                        
                        m_ARPointCloudManager.SetTrackablesActive(false);
                        SetAllPlanesActive(false);
                        ArUI.SetActive(true);
                        
                    }
                }
            }
        }

    }

    public void ChangeArObject(GameObject NewArObject)
    {
        GameObject aux = arObject;
        arObject = Instantiate(NewArObject, aux.transform.position, aux.transform.rotation);
        Destroy(aux);
    }

    public void ChangeObjectToSpawn(GameObject NewArObject)
    {
        ObjectToSpawn = NewArObject;
    }

    void SetAllPlanesActive(bool value)
    {
        m_ARPlaneManager.enabled = value;
        
        foreach (var plane in m_ARPlaneManager.trackables)
            plane.gameObject.SetActive(value);
    }

    public void ResetScene()
    {
        if (arObject)
        {            
            ArUI.SetActive(false);

            Destroy(arObject);
            arObject = null;

            SetAllPlanesActive(true);
            m_ARPointCloudManager.SetTrackablesActive(true);
        }
    }


}
    