using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitingObjects : MonoBehaviour
{
    [SerializeField] private GameObject infoOrbit;
    [SerializeField] private GameObject arObject;
    [SerializeField] private float initialScale = 0.2f;

    [SerializeField] private GameObject orbitGameObject;

    private GameObject myOrbit;

    [SerializeField] private float orbitRadius = 1f;
    public List<GameObject> orbitingObjects = new List<GameObject>();
    private List<GameObject> previousOrbit = new List<GameObject>();

    public float animationTime = 0.75f;


    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.transform.localScale = new Vector3(initialScale, initialScale, initialScale);
    }

    private void Start()
    {
        if (orbitingObjects != null && orbitGameObject != null)
            StartCoroutine(Delay());
               

        Debug.Log(UserData.GetUserName());
        Debug.Log(UserData.GetCompanyName());
        Debug.Log(UserData.GetSalesforceAgent());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CreateOrbitingObjects()
    {
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        myOrbit = Instantiate(orbitGameObject, transform.position, Quaternion.identity);        
        myOrbit.transform.SetParent(this.transform);

        int num = orbitingObjects.Count;

        for (int i = 0; i < num; i++)
        {

            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = arObject.transform.position + spawnDir * orbitRadius; // orbitRadius is just the distance away from the point

            /* Now spawn */
            var objectToSpawn = Instantiate(orbitingObjects[i], spawnPos, Quaternion.identity) as GameObject;            

            /* Set Parent */
            objectToSpawn.transform.SetParent(myOrbit.transform);

            /* Rotate the enemy to face towards player */
            objectToSpawn.transform.LookAt(arObject.transform.position);

            /* Adjust height */
            objectToSpawn.transform.Translate(new Vector3(0, objectToSpawn.transform.localScale.y/0.55f, 0));

        }

        this.gameObject.transform.localScale = new Vector3(initialScale, initialScale, initialScale);

        foreach (Transform child in myOrbit.transform)
        {
            //Set Scale
            child.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

            //Save targetPosition
            child.GetComponent<Sphere>().targetPosition = child.transform.position;

            //Move to the avatar position
            child.transform.position = this.transform.position;
        }

    }

    public void ChangeOrbitingObjects(List<GameObject> gameObjects)
    {
        orbitingObjects.Clear();

        orbitingObjects.AddRange(gameObjects);

        if (myOrbit != null)
            Destroy(myOrbit);

        initialScale = this.gameObject.transform.localScale.x;

        CreateOrbitingObjects();
    }

    public void ChangeToPreviousOrbit()
    {
        StartCoroutine(ChangeWithDelay());
    }


    public void SetPreviousOrbit()
    {
        if (previousOrbit != null)
            previousOrbit.Clear();

        previousOrbit.AddRange(orbitingObjects);
    }

    public void SetInfoOrbit(GameObject gameObject)
    {
        foreach (Transform child in infoOrbit.transform) //Destroy children
        {
            Destroy(child.gameObject);
        }

        GameObject newGameObject = Instantiate(gameObject);

        //Disable interaction
        if (newGameObject.GetComponent<Sphere>() != null)
            newGameObject.GetComponent<Sphere>().enabled = false;


        /* Set Parent */
        newGameObject.transform.SetParent(infoOrbit.transform);
        newGameObject.transform.localPosition = new Vector3 (0f, 0f, 0f);
        newGameObject.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        newGameObject.transform.localScale = new Vector3 (0.7f, 0.7f, 0.7f);

    }

    IEnumerator ChangeWithDelay()
    {
        foreach (Transform child in myOrbit.transform)
        {
            child.GetComponent<Sphere>().ExecuteAnimation(animationTime);
            Debug.Log(child.name);
        }

        yield return new WaitForSeconds(animationTime);

        foreach (Transform child in infoOrbit.transform) //Destroy children
        {
            Destroy(child.gameObject);
        }


        if (previousOrbit != null)
        {
            ChangeOrbitingObjects(previousOrbit);
        }


    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        CreateOrbitingObjects();
    }


}
