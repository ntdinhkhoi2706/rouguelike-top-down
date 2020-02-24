using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Craft")]
public class WeaponCraft : ScriptableObject
{
    public GunPickup weapon = null;
    private int count=0;
    public List<MaterialNeeded> needed = new List<MaterialNeeded>();
    public int Count
    {
        get => PlayerPrefs.GetInt(this.name + "count", count);
        set => PlayerPrefs.SetInt(this.name + "count", value);
    }
}


[System.Serializable]
public class MaterialNeeded
{
    public GameObject item;
    public int count;
}
