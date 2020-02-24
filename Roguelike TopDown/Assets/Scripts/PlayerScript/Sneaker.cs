using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneaker :PlayerController
{
    private bool buff = false;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        
        base.Update();
    }

    public override void Skill()
    {
        Vector3 tmp = WeaponManager.Instance.current_Weapon.transform.localScale;
        tmp.x = 2;
        tmp.y = 2;
        WeaponManager.Instance.current_Weapon.transform.localScale = tmp;
        if(!buff)
        {
            WeaponManager.Instance.current_Weapon.defaultConfig.damage *= 2;
            buff = true;
        }
        
    }

    public override void EndSkill()
    {
        Vector3 tmp = WeaponManager.Instance.current_Weapon.transform.localScale;
        tmp.x = 1;
        tmp.y = 1;
        WeaponManager.Instance.current_Weapon.transform.localScale = tmp;
        WeaponManager.Instance.current_Weapon.defaultConfig.damage /= 2;
        buff = false;
    }
}
