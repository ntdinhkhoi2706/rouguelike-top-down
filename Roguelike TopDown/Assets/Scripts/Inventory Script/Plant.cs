using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Plant")]
public class Plant : ScriptableObject
{
    public PlantReward reward = null;
    public Sprite icon = null;
    public string nameBuff = "Default";
    private int count = 0;

    public bool HaveItem
    {
        get => PlayerPrefs.GetInt(this.name, 0) == 1;
        set => PlayerPrefs.SetInt(this.name, value ? 1 : 0);
    }

    public int Count
    {
        get => PlayerPrefs.GetInt(this.name + "count", count);
        set => PlayerPrefs.SetInt(this.name + "count", value);
    }

}
