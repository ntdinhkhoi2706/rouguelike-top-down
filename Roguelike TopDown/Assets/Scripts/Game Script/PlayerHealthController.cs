using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;

    private float damageInvincLenghth = 1f;
    private float invinCount;

    private float restoreArmorLength=4;
    private float restoreCounter=4;


    private bool isHited;
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int CurrentArmor { get; set; }
    public int MaxArmor { get; set; }
    public int CurrentMana { get ; set; }
    public int MaxMana { get; set; }

    
    void Awake()
    {
        
        Instance = this;
    }

    void Start()
    {

        


       
        MaxHealth = CharacterTracker.Instance.MaxHealth;
        CurrentHealth = CharacterTracker.Instance.CurrentHealth;

        MaxArmor = CharacterTracker.Instance.MaxArmor;
        CurrentArmor = CharacterTracker.Instance.CurrentArmor;

        MaxMana = CharacterTracker.Instance.MaxMana;
        CurrentMana = CharacterTracker.Instance.CurrentMana;

        UIController.Instance.healthStat.Initilize(CurrentHealth, MaxHealth);
        UIController.Instance.armorStat.Initilize(CurrentArmor, MaxArmor);
        UIController.Instance.manaStat.Initilize(CurrentMana, MaxMana);

      
    }

    void Update()
    {
        if(invinCount >0)
        {
            invinCount -= Time.deltaTime;

            if(invinCount <=0)
            {
                PlayerController.Instance.TheBody.color = Color.white;
            }
        }

        if(CurrentArmor < MaxArmor)
        {
            if(restoreCounter >0)
            {
                restoreCounter -= Time.deltaTime;
                if(restoreCounter <0)
                {
                    if(isHited)
                    {
                        restoreCounter = restoreArmorLength;
                        isHited = false;
                        return;
                    }
                    CurrentArmor++;
                    UIController.Instance.armorStat.CurrentValue = CurrentArmor;
                    restoreCounter = 2;
                }
            }
        }

        
    }

    


    public void DamagePlayer()
    {
        
        if (invinCount <= 0)
        {
            isHited = true;
            if (CurrentArmor>0)
            {
                CurrentArmor--;
            }else
            {
                CurrentHealth--;
            }
            AudioManager.Instance.PlaySFX(8);

            invinCount = damageInvincLenghth;

            PlayerController.Instance.TheBody.color = new Color
                (PlayerController.Instance.TheBody.color.r, 0.05f, 0.05f, PlayerController.Instance.TheBody.color.a);
            if (CurrentHealth <= 0)
            {
                AudioManager.Instance.PlaySFX(7);
                PlayerController.Instance.CanMove = false;
                PlayerController.Instance.gameObject.SetActive(false);
                SceneManager.LoadScene("Win");
                AudioManager.Instance.PlayGameOver();
            }
            UIController.Instance.armorStat.CurrentValue = CurrentArmor;
            UIController.Instance.healthStat.CurrentValue = CurrentHealth; 
        }
    }


    

    public void IncreaseMana(int amount)
    {
        CurrentMana += amount;
        if(CurrentMana>= MaxMana)
        {
            CurrentMana = MaxMana;
        }
        UIController.Instance.manaStat.CurrentValue = CurrentMana;
    }

    public void DecreaseMana(int amount)
    {
        CurrentMana -=amount;
        if(CurrentMana<=0)
        {
            CurrentMana = 0;
        }
        UIController.Instance.manaStat.CurrentValue = CurrentMana;
    }

    public void MakeInvincible(float length)
    {
        invinCount = length;
        PlayerController.Instance.TheBody.color = new Color
                (PlayerController.Instance.TheBody.color.r, PlayerController.Instance.TheBody.color.g, PlayerController.Instance.TheBody.color.b, 0.5f);
    }

    public void HealthPlayer(int healthAmount)
    {
        CurrentHealth += healthAmount;

        if(CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UIController.Instance.healthStat.CurrentValue = CurrentHealth;
    }

    public void InCreaseMaxHealth(int amount)
    {
        if(CurrentHealth==MaxHealth)
        {
            MaxHealth += amount;
            CurrentHealth = MaxHealth;
        }else
        {
            MaxHealth += amount;
            CurrentHealth += amount / 2;
        }
        if (CurrentHealth >= MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        UIController.Instance.healthStat.MyMaxValue = MaxHealth;
        UIController.Instance.healthStat.CurrentValue = CurrentHealth;

    }

        
}
