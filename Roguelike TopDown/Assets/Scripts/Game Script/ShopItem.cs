using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    private bool isRestoreHealth=false;
    [SerializeField]
    private bool isRestoreMana=false;
    [SerializeField]
    private bool isWeapon=false;
    [SerializeField]
    private WeaponController[] weaponItems = null;
    [SerializeField]
    private SpriteRenderer sr=null;
    [SerializeField]
    private Text infoText=null;

    private int health, mana;

    private bool inBuyZone;

    private int itemCost;



    private WeaponController theGun;
    

    void Start()
    {
        if(isWeapon)
        {
            itemCost =10+ (int) Random.Range(LevelManager.Instance.CurrentCoins * 0.75f, LevelManager.Instance.CurrentCoins * 1.25f);
            int selectGun = Random.Range(0, weaponItems.Length);
            theGun = weaponItems[selectGun];

            sr.sprite = theGun.gunUI;
            infoText.text = theGun.gunName + "\n - " +itemCost + "Gold - ";
        }

        if(isRestoreHealth)
        {
            itemCost = 10 + (int)Random.Range(LevelManager.Instance.CurrentCoins * 0.5f, LevelManager.Instance.CurrentCoins);
            health = Random.Range(2, 4);
            infoText.text = "Restore Health" + "\n - " + itemCost + "Gold - ";
        }
        if(isRestoreMana)
        {
            itemCost = 10 + (int)Random.Range(LevelManager.Instance.CurrentCoins * 0.5f, LevelManager.Instance.CurrentCoins);
            mana = Random.Range(80, 100);
            infoText.text = "Restore Health" + "\n - " + itemCost + "Gold - ";
        }
        if(GameManager.Instance.mainBuff.Count>0)
        {
            foreach(var buff in GameManager.Instance.mainBuff)
            {
                if(buff.nameBuff.Contains("SaleOff"))
                {
                    itemCost /= 2;
                }
            }
        }
    }


    void Update()
    {
        if(inBuyZone)
        {
            if(JoyStickCanvas.Instance.activity)
            {
                if (LevelManager.Instance.CurrentCoins >= itemCost)
                {
                    LevelManager.Instance.SpendCoins(itemCost);

                    if (isRestoreHealth)
                    {
                        PlayerHealthController.Instance.HealthPlayer(health);
                    }
                    if (isRestoreMana)
                    {
                        PlayerHealthController.Instance.IncreaseMana(mana);
                    }
                    if (isWeapon)
                    {     
                                WeaponManager.Instance.ChangeWeapon(theGun);                    
                    }
                    AudioManager.Instance.PlaySFX(10);
                    gameObject.SetActive(false);
                    inBuyZone = false;
                    
                    JoyStickCanvas.Instance.activity = false;
                }else
                {
                    AudioManager.Instance.PlaySFX(11);
                    JoyStickCanvas.Instance.activity = false;
                }
            }
                
        }
    }


    void OnTriggerStay2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeShotToActivity();
            inBuyZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            JoyStickCanvas.Instance.ChangeActivityToShot();
            inBuyZone = false;
            
        }
    }
}
