using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponController
{

    [SerializeField]
    private BaseShot shot = null;
    [SerializeField]
    private BaseShot secondShot = null;

    public override void ProcessAttack()
    {

        if (transform != null)
        {
            if(secondShot!=null)
            {
                int rd = Random.Range(0, 100);
                if(rd < WeaponManager.Instance.current_Weapon.defaultConfig.critChance)
                {
                    secondShot.Shot();
                }else
                {
                    shot.Shot();
                }
            }else
            {
                shot.Shot();
            }
        }
        base.ProcessAttack();
    }
}
