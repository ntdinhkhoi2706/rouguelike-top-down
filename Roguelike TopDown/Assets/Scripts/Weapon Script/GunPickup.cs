using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    
    public WeaponController theGun = null;
    public WeaponCraft craftWp = null;
    private bool canPickUp;


    void Update()
    {
                if(JoyStickCanvas.Instance)
                {
                    if (JoyStickCanvas.Instance.activity&& canPickUp)
                    {
                AudioManager.Instance.PlaySFX(5);
                WeaponManager.Instance.ChangeWeapon(theGun);

                    JoyStickCanvas.Instance.activity = false;
                    Destroy(gameObject);

            }
                    
                }
                
               

    }
    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            
            JoyStickCanvas.Instance.ChangeShotToActivity();
            canPickUp = true;
            UIController.Instance.gunInfo.SetActive(true);
            UIController.Instance.damageText.text = theGun.defaultConfig.damage.ToString();
            UIController.Instance.critText.text = theGun.defaultConfig.critChance.ToString();
            UIController.Instance.defectionText.text = theGun.defaultConfig.deflection.ToString();
            UIController.Instance.manaText.text = theGun.defaultConfig.mana.ToString();

        }
            
            
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            canPickUp = false;
            JoyStickCanvas.Instance.ChangeActivityToShot();
            UIController.Instance.gunInfo.SetActive(false);
        }
    }
    
}
