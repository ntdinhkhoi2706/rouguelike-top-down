﻿
using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Item")]
public class Items : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;

    private int count=0;
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
