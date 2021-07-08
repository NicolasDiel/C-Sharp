using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;        
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> itemsList = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (itemsList.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }

            itemsList.Add(item);

            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
            
        }
        return true;
    }

    public void Remove (Item item)
    {
        itemsList.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
