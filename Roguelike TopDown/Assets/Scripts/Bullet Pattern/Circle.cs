using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : BaseShot
{
    protected override void Awake()
    {
        
        base.Awake();
    }

    public override void Shot()
    {
        if(BulletNum <=0 || bulletSpeed <=0f)
        {
            return;
        }


        float shiftAngle = 360f / (float)BulletNum;

        for(int i=0;i<BulletNum;i++)
        {
            var bullet = GetBullet(transform.position, transform.rotation);
            if(bullet==null)
            {
                break;
            }

            float angle = shiftAngle * i;

            ShotBullet(bullet, bulletSpeed, angle);
            AutoReleaseBulletGameObject(bullet.gameObject);
        }
        FinishShot();
    }
}
