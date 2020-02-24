using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUpgrade 
{
    public int Price { get; private set; }
    public int Health { get; private set; }
    public int Armor { get; private set; }
    public int Mana { get; private set; }

    public int TimeCoolDown { get; private set; }

    public string UpgradeInfo { get; private set; }

    public CharacterUpgrade(int health,int armor,int mana,int timeCoolDown,string upgradeInfo,int price)
    {
        this.Health = health;
        this.Armor = armor;
        this.Mana = mana;
        this.TimeCoolDown = timeCoolDown;
        this.UpgradeInfo = upgradeInfo;
        this.Price = price;
    }

    
}
