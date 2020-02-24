using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NameWeapon
{
    Pistol,
    Shotgun,
    Laser,
    ComboHit,
    Melee
}

public class WeaponController : MonoBehaviour
{
    public WeaponConfig defaultConfig = null;
    public NameWeapon nameWeapon =NameWeapon.Pistol;

    public GameObject weaponPickup = null;

    public float holdPower=5;

    private float lastShot;

    public string gunName=null;
    public Sprite gunUI = null;

    private Animator anim;

    public bool usingAnim=false;
    void Awake()
    {
        if(usingAnim)
        anim = GetComponent<Animator>();
    }


    public void CallAttack()
    {
        if(Time.time > lastShot + defaultConfig.fireRate)
        {
            if(PlayerHealthController.Instance.CurrentMana>=0)
            {
                if(usingAnim)
                    anim.SetTrigger("Attack");

                AudioManager.Instance.PlaySFX(9);

                ProcessAttack();
                
                lastShot = Time.time;
            }
            
        }
    }

    public virtual void ProcessAttack()
    {
        PlayerHealthController.Instance.DecreaseMana(defaultConfig.mana);
    }
}
