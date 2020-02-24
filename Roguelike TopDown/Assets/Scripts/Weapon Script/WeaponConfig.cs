using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeAttack
{
    Click,
    Hold
}
[System.Serializable]
public class WeaponConfig 
{
    public TypeAttack typeAttack;

    [Range(0, 10)]
    public float fireRate=5;

    [Range(0, 100)]
    public float damage=5;

    [Range(0, 100)]
    public float critChance=5;

    [Range(0, 100)]
    public float deflection=5;

    [Range(0, 20)]
    public int mana=5;
   
}
