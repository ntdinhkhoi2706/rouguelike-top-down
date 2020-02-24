using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public List<Items> items = new List<Items>();
    public List<ItemPickup> itemsPickup = new List<ItemPickup>();
    public int space = 20;
    void Awake()
    {
        Instance = this;
        foreach(var item in itemsPickup)
        {
            if(item.item.HaveItem)
            {
                items.Add(item.item);
            }
        }
    }


    public bool Add(Items item)
    {
        if (items.Count >= space)
        {
            return false;
        }

        if (!item.HaveItem)
        {
            items.Add(item);
            item.Count++;
            item.HaveItem = true;
        }
        else
        {
            item.Count++;
        }


        return true;
    }
    public void Remove(Items item)
    {
        if(item.Count <=0)
        {
            item.HaveItem = false;
            items.Remove(item);
        }
        
    }
}
