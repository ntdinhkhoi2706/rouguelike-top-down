using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diggle : PlayerController
{
    private float timeBtwShot=0.2f;
    public float shotDuration;
    private float holdPowerDefault;
    private bool isShooting=true;
    private float timeBtwCounter;
    public override void Start()
    {
        timeBtwCounter = timeBtwShot;
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Skill()
    {
        int count = 0;
        if (weaponManager.current_Weapon.defaultConfig.typeAttack == TypeAttack.Hold)
        {
            while (count < shotDuration)
            {
                if (timeBtwCounter > 0)
                {
                    weaponManager.Attack();
                    weaponManager.Hold();
                    weaponManager.holdPower = 5;
                    timeBtwCounter -= Time.deltaTime;
                    if (timeBtwCounter <= 0)
                    {
                        weaponManager.DropToAttack();
                        weaponManager.Attack();
                        count++;
                        weaponManager.holdPower = 0;
                        timeBtwCounter = timeBtwShot;
                    }

                }
            }
        }else
        {
            
            if(count<shotDuration)
            {
                if(timeBtwCounter>0)
                {
                    timeBtwCounter -= Time.deltaTime;
                    if(timeBtwCounter<=0)
                    {
                        print("a");
                        weaponManager.Attack();
                        count++;
                        timeBtwCounter = timeBtwShot;
                    }
                }
                
                
            }
        }
    }

    public override void EndSkill()
    {
       // weaponManager.current_Weapon.holdPower = holdPowerDefault;
    }
}
