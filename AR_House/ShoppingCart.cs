using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShoppingCart : MonoBehaviour
{
    [SerializeField] GameObject ArSessionOrigin;
    public Text totalPriceText;

    public Button buttonShopping;

    public GameObject prefabObjectToBuy;
    public int totalPrice = 0;

    public List<GameObject> objectsShoppingCart = new List<GameObject>();
    public List<GameObject> availableObjects = new List<GameObject>();



    public void CreateObjectToCart()
    {
        List<GameObject> tempListObjects = ArSessionOrigin.GetComponent<ChangeObjects>().objectsToChange;
        
        if (tempListObjects != null)
        {
            for (int i = 0; i < tempListObjects.Count; i++)
            {
                if (tempListObjects[i].activeSelf)
                {
                    foreach (var obj in availableObjects)
                    {
                        if (obj.name == tempListObjects[i].name)
                        {
                            if (SizeCart() > 0)
                            {
                                foreach (var item in objectsShoppingCart)
                                {
                                    Destroy(item);
                                }
                                objectsShoppingCart.Clear();
                            }        
                            
                            if (obj.name == "Chair0")
                            {
                                ClearCart();
                                break;
                            }
                                     
                            GameObject newCartObject = Instantiate(prefabObjectToBuy, prefabObjectToBuy.transform.position, prefabObjectToBuy.transform.rotation);

                            newCartObject.transform.SetParent(transform, false);

                            //Get info about the object
                            newCartObject.GetComponent<ObjectInCart>().spriteObjectToBuy.sprite = obj.GetComponent<InfoObject>().chairSprite;
                            newCartObject.GetComponent<ObjectInCart>().nameObjectToBuy.text = obj.GetComponent<InfoObject>().chairName;
                            newCartObject.GetComponent<ObjectInCart>().priceObjectToBuy.text = "$ " + obj.GetComponent<InfoObject>().chairPrice;
                            newCartObject.GetComponent<ObjectInCart>().price = System.Convert.ToInt32(obj.GetComponent<InfoObject>().chairPrice);


                            AddToCart(newCartObject);
                            Debug.Log(obj.name);
                        }
                    }                    
                                   
                }
            }
        }
        
    }

    public void AddToCart(GameObject obj)
    {
        objectsShoppingCart.Add(obj);
    }

    public void RemoveToCart(GameObject obj)
    {
        Destroy(obj);
        objectsShoppingCart.Remove(obj);
    }

    private int SizeCart()
    {
        return objectsShoppingCart.Count;
    }

    public void ClearCart()
    {
        foreach (var obj in objectsShoppingCart)
        {
            Destroy(obj);
        }
        objectsShoppingCart.Clear();


        ArSessionOrigin.GetComponent<ChangeObjects>().ChangeChair("Chair0");
    }

    public void CalculateTotal()
    {
        totalPrice = 0;

        foreach (GameObject obj in objectsShoppingCart)
        {
            int tempPrice = obj.GetComponent<ObjectInCart>().price;
            totalPrice += tempPrice;
        }

        Debug.Log(totalPrice.ToString());
        totalPriceText.text = "$ " + totalPrice.ToString();
    }

    public void ToggleActivation()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            buttonShopping.image.color = Color.white;
        }
        else
        {
            gameObject.SetActive(true);
            buttonShopping.image.color = new Color(1f, 0.9016038f, 0.4669811f);
        }
        
    }
}
