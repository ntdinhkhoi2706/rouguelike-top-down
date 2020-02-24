using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker Instance;

    private int maxHealth, currentHealth;
    private int maxArmor, currentArmor;
    private int maxMana, currentMana;

    private int coin;

    public int Coin { get => coin; set => coin = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxArmor { get => maxArmor; set => maxArmor = value; }
    public int CurrentArmor { get => currentArmor; set => currentArmor = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public int CurrentMana { get => currentMana; set => currentMana = value; }

    void Awake()
    {
        Instance = this;
    }
    

}
