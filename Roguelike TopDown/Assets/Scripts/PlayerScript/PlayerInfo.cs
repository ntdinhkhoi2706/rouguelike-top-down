using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{

    [SerializeField]
    private int health=0;
    [SerializeField]
    private int armor=0;
    [SerializeField]
    private int mana=0;
    [SerializeField]
    private int timeCoolDown=0;

    [SerializeField]
    private Stat statsHealth=null;
    [SerializeField]
    private Stat statsArmor=null;
    [SerializeField]
    private Stat statsMana=null;

    [SerializeField]
    private Text costUpgradeText=null;
    public Text upgradeInfo=null;

    [SerializeField]
    private GameObject upgradeBtn=null;

    private int level = 1;

    


    public int Health
    {
        get => PlayerPrefs.GetInt(this.name + "health", health);
        set => PlayerPrefs.SetInt(this.name + "health", value);
    }
    public int Armor
    {
        get => PlayerPrefs.GetInt(this.name + "armor", armor);
        set => PlayerPrefs.SetInt(this.name + "armor", value);
    }
    public int Mana
    {
        get => PlayerPrefs.GetInt(this.name + "mana", mana);
        set => PlayerPrefs.SetInt(this.name + "mana", value);
    }

    public int Level
    {
        get => PlayerPrefs.GetInt(this.name + "level", level);
        set => PlayerPrefs.SetInt(this.name + "level", value);
    }

    public int TimeCoolDown
    {
        get => PlayerPrefs.GetInt(this.name + "time", timeCoolDown);
        set => PlayerPrefs.SetInt(this.name + "time", value);
    }
    

    private CharacterUpgrade[] CharUpgrade;
    
    public CharacterUpgrade NextUpgrade
    {
        get
        {
            if (CharUpgrade.Length > Level - 1)
            {

                return CharUpgrade[Level - 1];

            }
            return null;
        }
    }
   

    void Awake()
    {
        CharUpgrade = new CharacterUpgrade[]
        {
            new CharacterUpgrade(Health+1,Armor,Mana,TimeCoolDown,"Health + 1",500),
            new CharacterUpgrade(Health+1,Armor+1,Mana,TimeCoolDown,"Armor + 1",1000),
            new CharacterUpgrade(Health+1,Armor+1,Mana+20,TimeCoolDown,"Mana + 20",1500),
            new CharacterUpgrade(Health+1,Armor+1,Mana+20,TimeCoolDown-2,"Time CoolDown -2 ",2000)
        };
        
    }

    void Start()
    {
        statsHealth.Initilize(Health, 10) ;
        statsArmor.Initilize(Armor, 10);
        statsMana.Initilize(Mana, 300);
        if(Level <= CharUpgrade.Length)
        {
            costUpgradeText.text = NextUpgrade.Price.ToString();
            upgradeInfo.text = NextUpgrade.UpgradeInfo.ToString();
            upgradeInfo.gameObject.SetActive(true);
        }   
    }
    public void Upgrade()
    { 
      if (Level <= CharUpgrade.Length )
        {
            upgradeInfo.text = NextUpgrade.UpgradeInfo.ToString();
            costUpgradeText.text = NextUpgrade.Price.ToString();
            if (GameManager.Instance.Wallet >= NextUpgrade.Price)
            {
                GameManager.Instance.Wallet -= NextUpgrade.Price;
                this.Health = NextUpgrade.Health;
                statsHealth.CurrentValue = NextUpgrade.Health;
                this.Armor = NextUpgrade.Armor;
                statsArmor.CurrentValue = NextUpgrade.Armor;
                this.Mana = NextUpgrade.Mana;
                statsMana.CurrentValue = NextUpgrade.Mana;
                this.TimeCoolDown = NextUpgrade.TimeCoolDown;
                
                Level++;
                
                if (Level-1>= CharUpgrade.Length)
                {
                    upgradeInfo.gameObject.SetActive(false);
                    upgradeBtn.SetActive(false);
                }

                if (Level <= CharUpgrade.Length)
                {
                    upgradeInfo.text = NextUpgrade.UpgradeInfo.ToString();
                    costUpgradeText.text = NextUpgrade.Price.ToString();
                }
                UIController.Instance.walletText.text = GameManager.Instance.Wallet.ToString();
            }
        }
           
        
        
    }



}
