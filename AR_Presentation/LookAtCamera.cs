using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private GameObject arCamera;

    // Start is called before the first frame update
    void Start()
    {
        arCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (arCamera != null)
        {        
            Vector3 targetPosition = new Vector3(arCamera.transform.position.x, transform.position.y, arCamera.transform.position.z);

            transform.LookAt(targetPosition);
        }
    }
}