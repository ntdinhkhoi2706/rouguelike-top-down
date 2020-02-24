using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInventory : MonoBehaviour
{
    public static PlantInventory Instance;

    public List<Plant> items = new List<Plant>();
    public List<PlantPickup> itemsPickup = new List<PlantPickup>();
    public int space = 20;
    void Awake()
    {
        Instance = this;
        foreach (var item in itemsPickup)
        {
            if (item.item.HaveItem)
            {
                items.Add(item.item);
            }
        }
    }


    public bool Add(Plant item)
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

    public void Remove(Plant item)
    {
        if(item.Count<=0)
        {
            item.HaveItem = false;
            items.Remove(item);
        }
        
   
    }
}
