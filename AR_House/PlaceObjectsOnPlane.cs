using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField] ARRaycastManager m_RaycastManager;
    [SerializeField] ARPlaneManager m_ARPlaneManager;

    GameObject measureParent = null;
    GameObject m_ObjectToPlane;
    public GameObject arObject = null;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    public Camera ArCamera;
    public GameObject UiZoom;
    public GameObject Obj_TutorialController;

    [Header("ArObject")]
    public float maxScale = 2.0f;
    public float minScale = 0.5f;

    [Header("Movement")]
    public float speed = 2.0f;

    void Awake()
    {
        GameObject aux = GameObject.FindGameObjectWithTag("GameController");
        m_ObjectToPlane = aux.GetComponent<HouseInfo>().GetCurrentHouse();
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

                        arObject = Instantiate(m_ObjectToPlane, hitPose.position, hitPose.rotation);

                        UiZoom.SetActive(true);

                        SetAllPlanesActive(false);

                    }
                }
            }
        }

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
            SetAllPlanesActive(true);
            //musicManager.Stop("sceneMusic");

            Destroy(arObject);
            arObject = null;

            this.gameObject.GetComponent<ChangeObjects>().objectsToChange.Clear();
        }
    }


    public void SetScale1_1()
    {
        if (arObject != null)
        {
            arObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void Move()
    {
        Vector3 tempVector = new Vector3(arObject.transform.localPosition.x + ArCamera.transform.forward.x * -speed * Time.deltaTime,
                                        arObject.transform.localPosition.y,
                                        arObject.transform.localPosition.z + ArCamera.transform.forward.z * -speed * Time.deltaTime);

        arObject.transform.localPosition = tempVector;
    }

    public void UpdateAllMeasureStuff()
    {
        measureParent = GameObject.FindGameObjectWithTag("Measure");

        if (measureParent != null)
        {
            foreach (Transform child in measureParent.transform)
            {
                child.GetComponent<Measure>().MeasureStuff();
            }
        }
    }

    public void ToggleAllMeasureStuff()
    {
        if (measureParent != null)
        {
            measureParent.SetActive(!measureParent.activeSelf);
        }
    }


}
    