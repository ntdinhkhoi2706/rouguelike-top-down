using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : WeaponController
{
    [SerializeField]
    private LaserLine laser = null;
    [SerializeField]
    private ChainLightning lightning = null;

    [SerializeField]
    private int wayNum=5;
    [SerializeField]
    private float betweenAngle=10;
    [SerializeField]
    private Transform spawnPoint = null;

    

    LaserLine[] lasers;
    ChainLightning[] lightnings;
    public enum TypeAttack
    {
        Hold,
        Click
    }

    public TypeAttack typeAttack;

    void Start()
    {
        lasers = new LaserLine[wayNum];
        lightnings = new ChainLightning[wayNum];
    }


    public override void ProcessAttack()
    {
        int wayIndex = 0;
        for (int i = 0; i < wayNum; i++)
        {
            if(laser!=null)
            {
                if (lasers[i] == null)
                {
                    lasers[i] = Instantiate(laser,spawnPoint.position,Quaternion.identity, spawnPoint.transform);
                    lasers[i].gameObject.GetComponent<Animator>();
                }
            }
            if (lightning != null)
            {
                if (lightnings[i] == null)
                {
                    lightnings[i] = Instantiate(lightning, spawnPoint.position,Quaternion.identity, transform);
                }
            }


            float centerAngle = PlayerController.Instance.RotateGun();

            float baseAngle = wayNum % 2 == 0 ? centerAngle - (betweenAngle / 2f) : centerAngle;

            float angle = MyUtil.GetShiftedAngle(wayIndex, baseAngle, betweenAngle);

            if (lightnings[i] != null)
            {
                lightnings[i].transform.SetEulerAnglesZ(angle);
                if (typeAttack == TypeAttack.Hold)
                {
                    lightnings[i].laserOn = true;
                }
                if(typeAttack == TypeAttack.Click)
                {
                    lightnings[i].laserOn = true;
                }

            }
            

            if (lasers[i] != null)
            {
                lasers[i].transform.SetEulerAnglesZ(angle);
                if (typeAttack == TypeAttack.Hold)
                {
                    lasers[i].canFire = true;
                }

                if (typeAttack == TypeAttack.Click)
                {
                    lasers[i].anim.SetBool("StartLaser", true);
                }
            }
            

            wayIndex++;
            wayIndex=wayIndex <= wayNum ? wayIndex : 0;

        }



        base.ProcessAttack();

    }


}


