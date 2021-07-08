using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Sphere : MonoBehaviour
{

    public List<GameObject> subSpheres = new List<GameObject>();

    private Camera arCamera;

    [HideInInspector] public bool selected = false;

    private float scaleUnselected = 0.75f;
    private float scaleSelected = 1.5f;

    [HideInInspector] public Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        StartAnimation(0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && arCamera != null)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.gameObject == this.gameObject)
                    {
                        if (selected)
                        {
                            ExecuteAction();
                        }
                        else
                            SelectButton();
                    }
                }
            }
        }
    }

    public void SelectButton()
    {
        GameObject parent = this.transform.parent.gameObject;

        foreach (Transform child in parent.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleUnselected, scaleUnselected, scaleUnselected);
            child.GetComponent<Sphere>().selected = false;
        }


        this.gameObject.transform.localScale = new Vector3(scaleSelected, scaleSelected, scaleSelected);
        selected = true;

    }

    public virtual void ExecuteAction()
    {
        GameObject Avatar = this.transform.parent.parent.gameObject;
        float time = Avatar.GetComponent<OrbitingObjects>().animationTime;

        GameObject parent = this.transform.parent.gameObject;


        foreach (Transform child in parent.transform)
        {
            child.GetComponent<Sphere>().ExecuteAnimation(time);
        }

        StartCoroutine(ActionWithAnimation(time));

    }

    public void ExecuteAnimation(float time)
    {
        GameObject Avatar = this.transform.parent.parent.gameObject;
        EndSphereAnimation Script = this.gameObject.GetComponent<EndSphereAnimation>();

        Script.enabled = true;

        Vector3 targetpos = Avatar.transform.position;

        foreach (Transform child in Avatar.transform)
        {
            if (child.name == "Model")
            {
                targetpos = child.position;
            }
        }

        Script.targetPos = targetpos;
        Script.time = time;
        Script.arcHeight = Avatar.transform.localScale.x * 1.5f;

    }

    private void StartAnimation(float time)
    {
        GameObject Avatar = this.transform.parent.parent.gameObject;
        StartSphereAnimation Script = this.gameObject.GetComponent<StartSphereAnimation>();

        Script.enabled = true;

        Vector3 targetpos = targetPosition;

        Script.targetPos = targetpos;
        Script.time = time;
        Script.arcHeight = Avatar.transform.localScale.x * 2f;
    }



    IEnumerator ActionWithAnimation(float secs)
    {
        yield return new WaitForSeconds(secs);     

        GameObject Avatar = this.transform.parent.parent.gameObject;

        Avatar.GetComponent<OrbitingObjects>().SetInfoOrbit(this.gameObject);
        Avatar.GetComponent<OrbitingObjects>().SetPreviousOrbit();
        Avatar.GetComponent<OrbitingObjects>().ChangeOrbitingObjects(subSpheres);

        Destroy(gameObject);
    }


}
