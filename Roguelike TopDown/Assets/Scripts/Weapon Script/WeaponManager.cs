using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;
    public List<WeaponController> weapons_Unlocked = null;
    public WeaponController[] total_Weapons = null;

    [HideInInspector]
    public WeaponController current_Weapon;

    private TypeAttack currentTypeAttack;
    private bool isShooting;
    [HideInInspector]
    public float holdPower { get; set; }
    [HideInInspector]
    public bool unHold=false,hold=false;

    [SerializeField]
    private GameObject holdPowerImg = null;
    [SerializeField]
    private Image[] img = null;
    [SerializeField]
    private Transform spawnPoint = null;

    private BaseShot shot;

    public int maxSlotWeapon = 2;

    public bool canShoot { get; set; }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        unHold = true;
        ChangeWeapon(total_Weapons[0]);
    }

    public void SwitchWeapon()
    {
        if(weapons_Unlocked.Count>1)
        {
            if (current_Weapon)
                Destroy(current_Weapon.gameObject);
            var wp = weapons_Unlocked[1];
            weapons_Unlocked[1] = weapons_Unlocked[0];
            weapons_Unlocked[0] = wp;
            current_Weapon = weapons_Unlocked[0];
            currentTypeAttack = weapons_Unlocked[0].defaultConfig.typeAttack;
            current_Weapon = Instantiate(weapons_Unlocked[0], spawnPoint.position, spawnPoint.rotation, spawnPoint);
            shot = weapons_Unlocked[0].GetComponentInChildren<BaseShot>();

            UIController.Instance.currentGun.sprite = weapons_Unlocked[0].gunUI;
            UIController.Instance.gunText.text = weapons_Unlocked[0].gunName;
        }
        
    }


    public void ChangeWeapon(WeaponController newWeapon)
    {
        if (current_Weapon)
            Destroy(current_Weapon.gameObject);
            

        current_Weapon = newWeapon;
        currentTypeAttack = newWeapon.defaultConfig.typeAttack;

        current_Weapon = Instantiate(newWeapon, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            

        shot = newWeapon.GetComponentInChildren<BaseShot>();
        weapons_Unlocked.Insert(0, newWeapon);

        if(weapons_Unlocked.Count>maxSlotWeapon)
        {
            Instantiate(weapons_Unlocked[1].weaponPickup, transform.position, Quaternion.identity);
            weapons_Unlocked.RemoveAt(1);

        }
        UIController.Instance.currentGun.sprite = weapons_Unlocked[0].gunUI;
        UIController.Instance.gunText.text = weapons_Unlocked[0].gunName;
    }

    public void Attack()
    { 
        if(currentTypeAttack == TypeAttack.Hold)
        {
            if(isShooting)
            {
                holdPowerImg.SetActive(true);
                holdPower += current_Weapon.holdPower* Time.deltaTime;
                for(int i=0;i<img.Length;i++)
                {
                    if(i<=holdPower)
                    {
                        img[i].color = Color.white;
                    }
                }
                if(holdPower<1)
                {
                    if(!hold)
                    {
                        holdPowerImg.SetActive(false);
                        holdPower = 0;
                        isShooting = false;
                    }
                }
                if (holdPower > 1)
                {
                    
                    if (!hold)
                    {
                        for(int i=0;i<img.Length;i++)
                        {
                            img[i].color = new Color(0.5647059f, 0.5647059f, 0.5647059f);
                        }
                        holdPowerImg.SetActive(false);
                        if(current_Weapon.nameWeapon != NameWeapon.Laser)
                        {
                            shot.BulletNum *= (int)holdPower;
                        }
                        
                        current_Weapon.CallAttack();
                        isShooting = false;
                        holdPower = 0;
                    }
                }
                if (holdPower >= 5)
                {
                    holdPower = 5;
                }
            }
           
            
            
        }
        else if(currentTypeAttack == TypeAttack.Click)
        {
                current_Weapon.CallAttack();
            
        }
        
    }
    

    public void Hold()
    {
        isShooting = true;
        hold = true;
    }
    public void DropToAttack()
    {
        hold = false;
    }
}
