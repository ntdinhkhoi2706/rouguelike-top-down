using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInventory : MonoBehaviour
{
    public static CraftInventory Instance;


    public List<WeaponCraft> items = new List<WeaponCraft>();
    public List<GunPickup> gunPickup = new List<GunPickup>();
    public int space = 20;
    void Awake()
    {
        Instance = this;
        foreach (var item in gunPickup)
        {
            if (item.craftWp.Count >=0)
            {
                items.Add(item.craftWp);
            }
        }
    }


}
